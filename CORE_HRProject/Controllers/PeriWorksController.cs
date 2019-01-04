using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CORE_HRProject.Data;
using CORE_HRProject.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Diagnostics;
using SHDocVw;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace CORE_HRProject.Controllers
{
    public class PeriWorksController : Controller
    {
        private ConnectionStrings connectionStrings { get; set; }
        public static string conn;
        private readonly ApplicationDbContext _context;
        public DataTable dt1 = new DataTable();
        public DataTable dt2 = new DataTable();
        public DataSet chkDataSet = new DataSet();
        public DataTable dtCheck = new DataTable();

        public PeriViewModel model = new PeriViewModel();

        public static string userId = "";
        public static string personalCode = "";
        public static string departmentCode = "";
        public static string departmentName = "";
        public static string userName = "";
        public static string reqNo = "";
        public static string workCode = "";
        public static string gViewSelLeft = "";
        public static string gViewSelName = "";
        public static string secondUser = "";

        public static string startDate = "";
        public static string endDate = "";

        public static string reason = "";

        public static string chkWrongId = "";

        private IHostingEnvironment _environment;

        public PeriWorksController(ApplicationDbContext context, IOptions<ConnectionStrings> settings, IHostingEnvironment environment)
        {
            _context = context;
            connectionStrings = settings.Value;
            conn = connectionStrings.DefaultConnection;
            _environment = environment;
            

        }

        [HttpPost]
        public async Task<ActionResult> Monitoring(DateTime from, DateTime to, string userId1, string reason, string pDepartmentVal, string pCodeVal1, string pCodeVal, string pCodeVal2, string chkDelHidden, string selectedChk, string selectedVal, string uploadHidden, ICollection<IFormFile> files, ICollection<IFormFile> files2, string secondUser, string uploadHiddenAfter, string checkAfterComponents, string pCodeVal3, string pCodeVal4, string pCodeVal5, string pCodeVal6, string selectedValSearch, string viewSelRight, string viewSelLeft, string uploadStartDate, string uploadStartDateAfter, string uploadPersonalCode, string uploadPersonalCodeAfter, string pUserVal, string selectedDepartment)
        {
            var uploadFolder = Path.Combine(_environment.WebRootPath, "files");
            string pViewSel = "";

            if (chkDelHidden == "c") // 삭제
            {
                bool delSuccessChk = deleteData(selectedChk);
               
                if (viewSelRight == "001") // 휴가를 선택
                {
                    if (pCodeVal3 == null) // 두번째 조회 조건을 선택하지 않았다면?
                    {
                        pCodeVal = "all";
                    }
                    else
                    {
                        pCodeVal = pCodeVal3;
                    }

                }
                else if (viewSelRight == "002") //공가를 선택.
                {
                    if (pCodeVal4 == null) // 두번째 조회 조건을 선택하지 않았다면?
                    {
                        pCodeVal = "all";
                    }
                    else
                    {
                        pCodeVal = pCodeVal4;
                    }
                }

                else if (viewSelRight == "003") //공가를 선택.
                {
                    if (pCodeVal5 == null) // 두번째 조회 조건을 선택하지 않았다면?
                    {
                        pCodeVal = "all";
                    }
                    else
                    {
                        pCodeVal = pCodeVal5;
                    }
                }

                else if (viewSelRight == "004") //공가를 선택.
                {
                    if (pCodeVal6 == null) // 두번째 조회 조건을 선택하지 않았다면?
                    {
                        pCodeVal = "all";
                    }
                    else
                    {
                        pCodeVal = pCodeVal6;
                    }
                }

                else if(viewSelRight == null) // 아무것도 선택하지 않았다면?
                {
                    viewSelRight = "000";
                    pCodeVal = "all";
                }
                
                if (delSuccessChk == true)
                {
                    return RedirectToAction("Index", new
                    {
                        id = userId1,
                        selCodeVal = "delChkSuccess",
                        pStartDate = from,
                        pEndDate = to,
                        departmentVal = pDepartmentVal,
                        codeVal = pCodeVal,
                        searchInform = new String[] { viewSelRight, pCodeVal }, // 1 연차 or 공가 / 2 세부 브라우저
                        pSelectedDepartment = selectedDepartment,
                        userVal = pUserVal
                    });
                }

                else
                {
                    return RedirectToAction("Index", new
                    {
                        id = userId1,
                        selCodeVal = "delChk",
                        pStartDate = from,
                        pEndDate = to,
                        departmentVal = pDepartmentVal,
                        codeVal = pCodeVal,
                        searchInform = new String[] { viewSelRight, pCodeVal }, // 1 연차 or 공가 / 2 세부 브라우저
                        pSelectedDepartment = selectedDepartment,
                        userVal = pUserVal
                    });
                }
                
            }

            else if (from != null && uploadHidden == null && uploadHiddenAfter == null) // 조회
            {
                pViewSel = viewSelRight; // 첫번째 대분류 조회 조건.
                
                if (pViewSel == "001") // 휴가를 선택
                {
                    if(pCodeVal3 == null ) // 두번째 조회 조건을 선택하지 않았다면?
                    {
                        pCodeVal = "all";
                    }
                    else
                    {
                        pCodeVal = pCodeVal3;
                    }
                    
                }
                else if(pViewSel == "002") //공가를 선택.
                {
                    if (pCodeVal4 == null) // 두번째 조회 조건을 선택하지 않았다면?
                    {
                        pCodeVal = "all";
                    }
                    else
                    {
                        pCodeVal = pCodeVal4;
                    }
                }

                else if (pViewSel == "003") //공가를 선택.
                {
                    if (pCodeVal5 == null) // 두번째 조회 조건을 선택하지 않았다면?
                    {
                        pCodeVal = "all";
                    }
                    else
                    {
                        pCodeVal = pCodeVal5;
                    }
                }

                else if (pViewSel == "004") //공가를 선택.
                {
                    if (pCodeVal6 == null) // 두번째 조회 조건을 선택하지 않았다면?
                    {
                        pCodeVal = "all";
                    }
                    else
                    {
                        pCodeVal = pCodeVal6;
                    }
                }

                else if (pViewSel == null) // 아무것도 선택하지 않았다면?
                {
                    pViewSel = "000";
                    pCodeVal = "all";
                }

                return RedirectToAction("Index", new
                {
                    id = userId1,
                    pStartDate = from,
                    pEndDate = to,
                    departmentVal = pDepartmentVal,
                    codeVal = pCodeVal,
                    //afterComponents = checkAfterComponents,
                    searchInform = new String[] { pViewSel, pCodeVal }, // 1 연차 or 공가 / 2 세부 브라우저
                    userVal = pUserVal,
                    pSelectedDepartment = selectedDepartment,
                });
            }

           else if(uploadHidden != null || uploadHiddenAfter != null) // 업로드
           {
                Random r = new Random();

                int ranDom1 = r.Next(1, 1000);
                int ranDom2 = r.Next(1, 1000);

                var nFiles = files.Count == 0 ? files2 : files;

                var fileName = "";

                pViewSel = viewSelRight; // 첫번째 대분류 조회 조건.

                if (pViewSel == "001") // 휴가를 선택
                {
                    if (pCodeVal3 == null) // 두번째 조회 조건을 선택하지 않았다면?
                    {
                        pCodeVal = "all";
                    }
                    else
                    {
                        pCodeVal = pCodeVal3;
                    }

                }
                else if (pViewSel == "002") //공가를 선택.
                {
                    if (pCodeVal4 == null) // 두번째 조회 조건을 선택하지 않았다면?
                    {
                        pCodeVal = "all";
                    }
                    else
                    {
                        pCodeVal = pCodeVal4;
                    }
                }

                else if (pViewSel == "003") //공가를 선택.
                {
                    if (pCodeVal5 == null) // 두번째 조회 조건을 선택하지 않았다면?
                    {
                        pCodeVal = "all";
                    }
                    else
                    {
                        pCodeVal = pCodeVal5;
                    }
                }

                else if (pViewSel == "004") //공가를 선택.
                {
                    if (pCodeVal6 == null) // 두번째 조회 조건을 선택하지 않았다면?
                    {
                        pCodeVal = "all";
                    }
                    else
                    {
                        pCodeVal = pCodeVal6;
                    }
                }

                else if (viewSelRight == "") // 아무것도 선택하지 않았다면?
                {
                    viewSelRight = "000";
                    pCodeVal = "all";
                }

                foreach (var file in nFiles)
                {
                    if (file.Length > 0)
                    {
                        fileName = userId1 + Convert.ToString(ranDom1) + Convert.ToString(ranDom2) + "_" + Path.GetFileName(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

                        using (var fileStream = new FileStream(Path.Combine(uploadFolder, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }

                string uploadNo = uploadHidden == null ? uploadHiddenAfter : uploadHidden;
                string pUploadDate = uploadStartDate == null ? uploadStartDateAfter : uploadStartDate;
                string uploadPersonal = uploadPersonalCode == null ? uploadPersonalCodeAfter : uploadPersonalCode;

                string isAfterChk = uploadHidden == null ? "c" : "";

                return RedirectToAction("Index", new
                {
                    id = userId1,
                    pStartDate = from,
                    pEndDate = to,
                    departmentVal = pDepartmentVal,
                    codeVal = pCodeVal,
                    fileName = fileName,
                    uploadReqNo = uploadNo,
                    isAfter = isAfterChk,
                    searchInform = new String[] { pViewSel, pCodeVal }, // 1 연차 or 공가 / 2 세부 브라우저
                    uploadDate = pUploadDate,
                    pPersonalCode = uploadPersonal,
                    pSelectedDepartment = selectedDepartment,
                    userVal = pUserVal
                });
            }

            else // 등록 
            {
                return RedirectToAction("Index", new
                {
                    id = userId1,
                    pWorkCode = pCodeVal,
                    pStartDate = from,
                    pEndDate = to,
                    pReason = reason,
                    pViewSelLeft = viewSelLeft,
                    pSecondUserId = secondUser == null ? "" : secondUser,
                });
            }

        }

        public ActionResult FileDownload(string fileName) // 조회에서 첨부된 파일인 경우
        {
            byte[] fileBytes;
            string chkName = "";

            chkName = fileName;
                fileBytes = System.IO.File.ReadAllBytes(
                    Path.Combine(_environment.WebRootPath, "files") + "\\" + fileName);
                return File(fileBytes, "application/octet-stream", fileName);
        }

        public ActionResult FileDownload2(string fileName) // 전자결재를 통한 파일 첨부일 경우
        {
            string[] temp = fileName.Split('/');
            int tempLength = temp.Length - 1;

            using (var client = new WebClient())
            {
                var content = client.DownloadData(fileName);
                string contentType = client.ResponseHeaders["content-type"];
                return File(content, "application/octet-stream", temp[tempLength]);
            }

        }


        // GET: PeriWorks
        public IActionResult Index(string id, string pWorkCode, string pStartDate, string pEndDate, string pReason, string pViewSelLeft, string userVal,
            string departmentVal, string codeVal, string url, string selCodeVal, string fileName, string uploadReqNo, string getFileError, string pSecondUserId, string isAfter, string afterComponents, string[] searchInform, string uploadDate, string pPersonalCode, string pSelectedDepartment)
        {
            //var model = new PeriViewModel();
            string[] startSplit;
            string[] endSplit;
            string nStart = "";
            string nEnd = "";
            DataTable dt1Browser = new DataTable();

            ViewBag.url = ""; // 기본은 초기화. 등록 버튼 클릭 시 url 들어감.
            ViewBag.codeVal = ""; // 기본은 초기화. 조회 버튼 클릭 시.
            ViewBag.departmentVal = ""; // 조회버튼 클릭 시
            ViewBag.searchFirst = "";
            ViewBag.searchSecond = "";
            ViewBag.secondUser = "";
            ViewBag.userId = "";
            ViewBag.pSelectedDepartment = pSelectedDepartment;

            if (id != null) // 
            {
                if (id != null)
                {
                    byte[] arr = System.Convert.FromBase64String(id);
                    string tId = System.Text.Encoding.UTF8.GetString(arr);

                    ViewBag.userId = tId;
                }
                
            }

            else if (id == null)
            {
                return NotFound();
            }

            
            if (pWorkCode == "0") // 등록 진행 후 리프레시 되는 화면에서 Viewbag에 삽입.
            {
                ViewBag.url = url;
            }
            
            // 공통, 날짜를 짤라 넣는 부분.
            if (pStartDate != null && pStartDate.Length != 8)
            {
                startSplit = pStartDate.Split("/");
                endSplit = pEndDate.Split("/");

                nStart = startSplit[2].Substring(0,4) + startSplit[0] + startSplit[1];
                nEnd = endSplit[2].Substring(0, 4) + endSplit[0] + endSplit[1];

                ViewBag.startDate = pStartDate == null ? "1" : nStart;
                ViewBag.endDate = pEndDate == null ? "1" : nEnd;

                if (departmentVal == null)
                {
                    ViewBag.nStartDate = ViewBag.startDate;
                    ViewBag.nEndDate = ViewBag.endDate;
                }
            }

           // baseId, 디코딩 된 id 저장 및 사용자 기본정보 불러오는 부분

            ViewBag.baseUserId = id;
            ViewBag.reason = pReason;
            

            byte[] data = Convert.FromBase64String(id);
            string decodedString = System.Text.Encoding.UTF8.GetString(data);
            
            dt1 = GetData(String.Format("select A.userName, A.userId, A.personalCode, A.departmentCode, A.departmentName, A.getRestOfday, A.etc, A.totalRestDay, A.usedRestDay, B.managerCode from PeriWork A LEFT OUTER JOIN selectInformation B on A.userId = B.managerCode where userId = '" + decodedString + "'"));
            DataRow drManager = dt1.Rows[0];
            
            if (drManager["managerCode"].ToString() != "")
            {
                ViewBag.isManager = 1;

            }

            else
            {
                ViewBag.isManager = 0;
            }

            if (dt1.Rows.Count != 0) // 기본 settings.
            {
                DataSet ds = getBrowsers(decodedString);
                dt1Browser = ds.Tables[0]; // set browser1
                DataTable dt2Browser = ds.Tables[1]; // set browser2 휴가
                DataTable dt3Browser = ds.Tables[2]; // set browser3 공가
                DataTable dt4Browser = ds.Tables[3]; // set browser4 전체
                DataTable dt5Browser = ds.Tables[4]; // set browser5 전체
                DataTable dt6Browser = ds.Tables[5]; // set browser6 id로 부서따서 조회 셀렉 옵션에 사용자 리스트 뿌리는 테이블.
                DataTable dt7Browser = ds.Tables[6]; // 병가
                DataTable dt8Browser = ds.Tables[7]; // 결근
                DataTable dtGarb = new DataTable(); // empty table.
                DataTable dtGarb2 = new DataTable(); // empty table.

                model.PeriHeaderModel = dt1Browser.AsEnumerable().Select(row =>
                new PeriHeaderModel
                {
                    departmentName = row["divName"].ToString(),
                    departmentCode = row["divCode"].ToString()
                });

                model.PeriHeaderModel2 = dt2Browser.AsEnumerable().Select(row =>
                new PeriHeaderModel2
                {
                    code = row["divName"].ToString(),
                    codeNumber = row["divCode"].ToString()
                });

                model.PeriHeaderModel3 = dt3Browser.AsEnumerable().Select(row =>
                new PeriHeaderModel3
                {
                    code = row["divName"].ToString(),
                    codeNumber = row["divCode"].ToString()
                });

                model.PeriHeaderModel7 = dt7Browser.AsEnumerable().Select(row =>
                new PeriHeaderModel7
                {
                    code = row["divName"].ToString(),
                    codeNumber = row["divCode"].ToString()
                });

                model.PeriHeaderModel8 = dt8Browser.AsEnumerable().Select(row =>
                new PeriHeaderModel8
                {
                    code = row["divName"].ToString(),
                    codeNumber = row["divCode"].ToString()
                });



                model.PeriHeaderModel4 = dt4Browser.AsEnumerable().Select(row =>
                new PeriHeaderModel4
                {
                    code = row["divName"].ToString(),
                    codeNumber = row["divCode"].ToString()
                });

                model.PeriHeaderModel5 = dt5Browser.AsEnumerable().Select(row =>
                new PeriHeaderModel5
                {
                    code = row["divName"].ToString(),
                    codeNumber = row["divCode"].ToString()
                });

                DataRow drTemp1 = dt6Browser.NewRow();
                drTemp1["userName"] = "[초기화]";
                drTemp1["personalCode"] = "0";

                dt6Browser.Rows.InsertAt(drTemp1, 0);

                //model.PeriHeaderModel6 = dt6Browser.AsEnumerable().Select(row =>
                //new PeriHeaderModel6
                //{
                //    userName = row["userName"].ToString(),
                //    personalCode = row["personalCode"].ToString()
                //});

                model.PeriBodyModel = dtGarb.AsEnumerable().Select(row =>
                new PeriBodyModel
                {
                    userName = "",
                    departmentName = "",
                    startDate = "",
                    endDate = "",
                    reason = "",
                    appYN_DESC = "",
                    hrYN_DESC = "",
                    personalCode = "",
                    cancelDESC = "",
                });


               model.PeriBodyModel2 = dtGarb2.AsEnumerable().Select(row =>
               new PeriBodyModel2
               {
                   userName = "",
                   departmentCode = "",
                   personalCode = "",
                   startDate = "",
                   endDate = "",
                   reason = "",
               });
                //////////////////////////////////////////////
                DataRow dr;

                dr = dt1.Rows[0];
                userId = dr["userId"].ToString();
                personalCode = dr["personalCode"].ToString();
                departmentName = dr["departmentName"].ToString();
                userName = dr["userName"].ToString();
                departmentCode = dr["departmentCode"].ToString();
                startDate = ViewBag.startDate;
                endDate = ViewBag.endDate;
                reason = ViewBag.reason;
                
                ViewBag.userId = dr["userId"];
                ViewBag.personalCode = dr["personalCode"];
                ViewBag.departmentName = dr["departmentName"];
                ViewBag.departmentCode = dr["departmentCode"];
                ViewBag.userName = dr["userName"];
                ViewBag.getRestOfDay = dr["getRestOfDay"];
                ViewBag.totalRestDay = dr["totalRestDay"];
                ViewBag.usedRestDay = dr["usedRestDay"];
                //////////////////////////////////////////////
                
                // 삭제 시, 
                if (selCodeVal == "delChk") // 삭제 미완료 시,
                {
                    ViewBag.chkDel = "c";
                }

                else if(selCodeVal == "delChkSuccess") // 삭제 완료 시,
                {
                    ViewBag.chkDel = "f";
                }
            }

            if(searchInform.Length != 0) //조회버튼을 눌렀을 경우,
            {
                if(userVal == null) // 사용자 전체 시,
                {
                    userVal = "0";
                }
                if(fileName != null) // 첨부파일 업로드가 진행된 경우, 조회로직 안에 첨부 로직을 포함.
                {   
                    updateFilePath(fileName, uploadReqNo, isAfter, uploadDate, pPersonalCode);
                }
                
                //조회 브라우저 다시 불러오게.
                ViewBag.startDate2 = pStartDate.Substring(6, 4) + "-" + pStartDate.Substring(0, 2) + "-" + pStartDate.Substring(3, 2);
                ViewBag.endDate2 = pEndDate.Substring(6, 4) + "-" + pEndDate.Substring(0, 2) + "-" + pEndDate.Substring(3, 2);
                ViewBag.userVal = userVal.Trim();
                ViewBag.departmentVal = departmentVal == null ? "" : departmentVal;

                // 기본 브라우저
                if(searchInform.Length != 0)
                {
                    ViewBag.searchFirst = searchInform[0]; // 000 or specific
                    ViewBag.searchSecond = searchInform[1]; // all or specific
                }

                // id, startDate, endDate, 부서, 코드
                DataSet ds = getLists(decodedString, startDate, endDate, Convert.ToString(ViewBag.departmentVal), codeVal, Convert.ToString(ViewBag.isManager), "0", userVal, dt1Browser);

                model.PeriBodyModel = ds.Tables[0].AsEnumerable().Select(row =>
                new PeriBodyModel
                {
                    reqNo = row["reqNo"].ToString(),
                    userName = row["userName"].ToString(),
                    departmentName = row["divName"].ToString(),
                    //startDate = row["startDate"].ToString().Substring(0, 4) + "-" + row["startDate"].ToString().Substring(4, 2) + "-" + row["startDate"].ToString().Substring(6, 2),
                    //endDate = row["endDate"].ToString().Substring(0, 4) + "-" + row["endDate"].ToString().Substring(4, 2) + "-" + row["endDate"].ToString().Substring(6, 2),
                    startDate = row["startDate"].ToString(),
                    endDate = row["endDate"].ToString(),
                    reason = row["reason"].ToString(),
                    appYN_DESC = row["appYN_DESC"].ToString(),
                    hrYN_DESC = row["hrYN_DESC"].ToString(),
                    cancelDESC = row["cancelDESC"].ToString(),
                    filePath = Convert.ToString(row["filePath"]),
                    afterFilePath = Convert.ToString(row["afterFilePath"]),
                    personalCode = Convert.ToString(row["personalCode"]),
                    gViewSelName = Convert.ToString(row["gViewSelName"])
                });
                
            }
            
            if (dt1.Rows.Count !=0)
            {
                return View(model);
            }
            else
            {
                return View();
            }
        }

        private void updateFilePath(string pFileName, string pReqNo, string isAfter, string pStartDate, string personalCode)
        {
            string constr = conn;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                if(isAfter == "c")
                {
                    cmd.CommandText = "UPDATE PeriReqList SET afterFilePath ='" + pFileName + "' WHERE reqNo ='" + pReqNo + "' AND startDate ='" + pStartDate + "' AND personalCode = '" + personalCode + "'";
                }
                
                else
                {
                    cmd.CommandText = "UPDATE PeriReqList SET filePath ='" + pFileName + "' WHERE reqNo ='" + pReqNo + "' AND startDate ='" + pStartDate + "' AND personalCode = '" + personalCode + "'";
                }
                cmd.ExecuteNonQuery();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OnGetApprove(string workCode, string userId, string personalCode, string workCodeFirst, string secondUser, string totalCount, string currentCount, string totalCount2, string currentCount2) // 상신
        {
            string jsonResult = "";
            string url = "";
            string base64Usr = "";
            ViewBag.personalCode = personalCode;
            ViewBag.userId = userId;

            string totalValue = "";
            string currentValue = "";
            //base64로 변환.
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(userId);
            base64Usr = System.Convert.ToBase64String(plainTextBytes);

            string tmpText1 = base64Usr;

            ViewBag.baseUserId = tmpText1;
            
            if(workCodeFirst == "001")
            {
                totalValue = totalCount;
                currentValue = currentCount;
            }

            else if(workCodeFirst == "002")
            {
                totalValue = totalCount2;
                currentValue = currentCount2;
            }

            //req no //
            DataTable dt = GetData("select * from periReqNoTable where userId = '" + ViewBag.userId + "'");

            if(dt.Rows.Count == 0) // 아예 없으면?
            {
                DataTable dtEmpty = new DataTable();

                insertReqNo(dtEmpty, false, workCode, workCodeFirst, secondUser, totalValue, currentValue);

            }

            else
            {
                insertReqNo(dt, true, workCode, workCodeFirst, secondUser, totalValue, currentValue);
            }
            
            url = ViewBag.url;

            jsonResult = JsonConvert.SerializeObject(dt, Formatting.None);
            
            return Json(new { pUrl = url });
        }

        public void insertApprovalData(string id, string workCode, string reqNo, string personalCode, string workCodeFirst, string secondUser, string totalValue, string currentValue)
        {
            DataTable dt = new DataTable();

            string constr = conn;
            string errMsg = "";

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_Peri_INSERTAPPR", con);
                    //cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@WORKGB", "INSERT"); // 대리 입력 시, userId에 secondUser를..
                    cmd.Parameters.AddWithValue("@userId", id);
                    cmd.Parameters.AddWithValue("@workCode", workCode);
                    cmd.Parameters.AddWithValue("@reqNo", reqNo);
                    cmd.Parameters.AddWithValue("@personalCode", personalCode);
                    cmd.Parameters.AddWithValue("@secondUser", secondUser);
                    cmd.Parameters.AddWithValue("@totalCount", totalValue);
                    cmd.Parameters.AddWithValue("@currentCount", currentValue);
                    cmd.ExecuteNonQuery();

                }
            }
            catch(Exception e)
            {
                errMsg = e.Message;
            }
            finally
            {
                if(errMsg == "")
                {
                    string tUrl;

                    if(workCodeFirst == "001")
                    {
                        tUrl = "http://gw.se-won.co.kr/UI/_EAPP/ERPRLogin.aspx?UserID=" + ViewBag.baseUserId + "&reqNo=" + reqNo + "&Lang=ko&EAID=15&gw_num=&htmltag=&NextUrl=%2fUI%2f_EAPP%2fEADocumentWrite.aspx%3fFormID%3d15&ErpDocTitle=";
                        ViewBag.url = tUrl;
                    }
                    else if(workCodeFirst == "002")
                    {
                        tUrl = "http://gw.se-won.co.kr/UI/_EAPP/ERPRLogin.aspx?UserID=" + ViewBag.baseUserId + "&reqNo=" + reqNo + "&Lang=ko&EAID=20&gw_num=&htmltag=&NextUrl=%2fUI%2f_EAPP%2fEADocumentWrite.aspx%3fFormID%3d20&ErpDocTitle=";
                        ViewBag.url = tUrl;
                    }
                    else if (workCodeFirst == "003")
                    {
                        tUrl = "http://gw.se-won.co.kr/UI/_EAPP/ERPRLogin.aspx?UserID=" + ViewBag.baseUserId + "&reqNo=" + reqNo + "&Lang=ko&EAID=24&gw_num=&htmltag=&NextUrl=%2fUI%2f_EAPP%2fEADocumentWrite.aspx%3fFormID%3d24&ErpDocTitle=";
                        ViewBag.url = tUrl;
                    }
                    else if (workCodeFirst == "004")
                    {
                        tUrl = "http://gw.se-won.co.kr/UI/_EAPP/ERPRLogin.aspx?UserID=" + ViewBag.baseUserId + "&reqNo=" + reqNo + "&Lang=ko&EAID=25&gw_num=&htmltag=&NextUrl=%2fUI%2f_EAPP%2fEADocumentWrite.aspx%3fFormID%3d25&ErpDocTitle=";
                        ViewBag.url = tUrl;
                    }

                }
            }
            
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OnGetWorkDay(string personalCode, string workCode)
        {
            // 사번으로 회사정보, 관리/생산직 정보를 알 수 있고, workCode로 근태코드별 정보를 알 수 있다.

            string jsonResult = "";

            string constr = conn;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_Peri_GETWORKDAY", con);
                    cmd.Connection = con;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@WORKGB", "SEARCH");
                    cmd.Parameters.AddWithValue("@personalCode", personalCode);
                    cmd.Parameters.AddWithValue("@workCode", workCode);
                    SqlDataAdapter dsAdapter = new SqlDataAdapter(cmd);
                    dsAdapter.Fill(ds);
                    
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }

                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }

            }
            
            dt = ds.Tables[0];

            jsonResult = JsonConvert.SerializeObject(dt, Formatting.None);
            return new JsonResult(jsonResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OnGetDocumentList(string workCode)
        {
            // workCode로 근태코드별 document list 정보를 불러온다.

            string jsonResult = "";

            string constr = conn;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_Peri_GETDOCULIST", con);
                    cmd.Connection = con;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@WORKGB", "SEARCH");
                    cmd.Parameters.AddWithValue("@workCode", workCode);
                    SqlDataAdapter dsAdapter = new SqlDataAdapter(cmd);
                    dsAdapter.Fill(ds);

                    //divname, etc1.
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }

                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }

            }

            dt = ds.Tables[0];

            jsonResult = JsonConvert.SerializeObject(dt, Formatting.None);
            return new JsonResult(jsonResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OnGetList(string userName, string departmentCode, string secondUser)
        {
            string jsonResult = "";

            DataSet ds = getBrowsers(secondUser);
            DataTable dt2 = ds.Tables[0];
            string etc2 = dt2.Rows[0]["etc2"].ToString();

            ds.Clear();

            ds = getModalBrowser(etc2, userName);

            DataTable dt = ds.Tables[0];

            //DataTable dt = GetData("select userId, departmentName, personalCode, userName from periWork where userName = '" + userName + "' AND departmentCode = '" + departmentCode + "'");
            
            jsonResult = JsonConvert.SerializeObject(dt, Formatting.None);

            return new JsonResult(jsonResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OnGetUserName(string departmentCode) // 부서 선택 시 사용자 받아오기.
        {
            string jsonResult = "";

            DataTable dt = GetData("select userName, personalCode from periWork where departmentCode = '" + departmentCode + "'");
            
            jsonResult = JsonConvert.SerializeObject(dt, Formatting.None);

            return new JsonResult(jsonResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OnGetRequestView(string personalCode)
        {
            string jsonResult = "";

            DataTable dt = GetData("select * from periWork where personalCode = '" + personalCode + "'");
            
            jsonResult = JsonConvert.SerializeObject(dt, Formatting.None);

            return new JsonResult(jsonResult);
        }

        // 임시 등록
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OnGetRequestTemp(string registerId, string workCode, string userName, string personalCode, string startDate, string endDate, string reason, string viewSelLeft, string viewSelVal)
        {
            bool isTrue = insertData(registerId, workCode, userName, personalCode, startDate, endDate, reason, viewSelLeft, viewSelVal);
        
            if(isTrue == false)
            {
                return Json(new { success = false, response = "error" });
            }

            else
            {
                return Json(new { success = true, response = "success" });
            }

            //return new JsonResult(jsonResult);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OnGetListTable(string userId, string workCode)
        {
            DataTable tempUserTable = GetData("SELECT * FROM PeriTempList WHERE registerId = '" + userId + "' AND workCode ='" + workCode + "' AND isApproval !='Y'");

            string jsonResult = "";

            jsonResult = JsonConvert.SerializeObject(tempUserTable, Formatting.None);



            return new JsonResult(jsonResult);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OnGetDeleteList(string personalCode, string startDate)
        {
            string jsonResult = "";

            deleteData(personalCode, startDate); // 리스트 삭제.


            return new JsonResult(jsonResult);

        }


        private DataSet getLists(string id, string startDate, string endDate, string departmentVal, string codeVal, string managerCode, string chkVal, string userVal, DataTable dt1Browser)
        {
            string constr = conn;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("departmentCode");
            
            string workGb = "SELECT";
            if (chkVal == "c")
            {
                workGb = "CHKSEL";
            }

            if(departmentVal == "" || departmentVal == null)
            {
                foreach(DataRow dr in dt1Browser.Select())
                {
                    DataRow dr2 = dt.NewRow();

                    dr2["departmentCode"] = dr["divCode"];

                    dt.Rows.Add(dr2);
                }
            }
            
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_Peri_GETLISTS", con);
                    cmd.Connection = con;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@WORKGB", workGb);
                    cmd.Parameters.AddWithValue("@userId", id);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.Parameters.AddWithValue("@departmentCode", departmentVal);
                    cmd.Parameters.AddWithValue("@codeVal", codeVal);
                    cmd.Parameters.AddWithValue("@managerCode", managerCode);
                    cmd.Parameters.AddWithValue("@searchFirst", ViewBag.searchFirst);
                    cmd.Parameters.AddWithValue("@userVal", userVal.Trim());
                    if (dt.Rows.Count != 0)
                    {
                        cmd.Parameters.AddWithValue("@tempDepartDt", dt);
                        cmd.Parameters.AddWithValue("@tempDepartDtChk", "c");
                    }
                        

                    SqlDataAdapter dsAdapter = new SqlDataAdapter(cmd);
                    dsAdapter.Fill(ds);

                    foreach(DataRow dr in ds.Tables[0].Select("useDate = '5'"))
                    {
                        dr.Delete();
                    }

                    ds.Tables[0].AcceptChanges();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }

                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }

                return ds;

            }
        }

        private DataSet getLists(string registerId, string personalCode, string startDate, string endDate)
        {
            string constr = conn;
            DataSet ds = new DataSet();

            string workGb = "SELECT";

            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_Peri_GETLISTS", con);
                    cmd.Connection = con;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@WORKGB", "CHKSEL_T");
                    cmd.Parameters.AddWithValue("@registerId", registerId);
                    cmd.Parameters.AddWithValue("@personalCode", personalCode);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    SqlDataAdapter dsAdapter = new SqlDataAdapter(cmd);
                    dsAdapter.Fill(ds);

                    foreach (DataRow dr in ds.Tables[0].Select("useDate = '5'"))
                    {
                        dr.Delete();
                    }

                    ds.Tables[0].AcceptChanges();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }

                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }

                return ds;

            }
        }
        
        private static DataTable GetData(string query)
        {
            string constr = conn;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;  
                        }
                    }
                }
            }
        }

        public static void deleteData(string personalCode, string startDate)
        {
            string constr = conn;

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                
                cmd.CommandText = "delete from PeriTempList where personalCode = '" + personalCode + "'" + " AND startDate ='" + startDate + "'";
                cmd.ExecuteNonQuery();

            }
        }
        public static bool deleteData(string selectedData)
        {
            //selectedData는 sql에 넘어가서 split으로 ','를 제거하여 처리한다.
            string [] selectedVal = selectedData.Split(',');

            string constr = conn;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;



                for (int i = 0; i < selectedVal.Length; i++)
                {
                    DataTable dtTemp = GetData("SELECT * FROM PeriReqList where reqNo = '" + selectedVal[i] + "' AND appYN != 'N'"); // 결재가 난 건이라면?
                    if (dtTemp.Rows.Count != 0)
                    {
                        return false;
                    }

                }
                    

                for (int i=0; i< selectedVal.Length; i++)
                {
                    cmd.CommandText = "delete from PeriReqList where reqNo = '" + selectedVal[i] + "'";
                    cmd.ExecuteNonQuery();
                    
                    cmd.CommandText = "delete from PeriTempList where reqNo = '" + selectedVal[i] + "'";
                    cmd.ExecuteNonQuery();
                }
                
                return true;
            }
        }

        public void insertReqNo(DataTable dt, bool isValue, string workCode, string workCodeFirst, string secondUser, string totalValue, string currentValue)
        {
            int newReqNo = 0;
            string temp;

            if (isValue)
            {
                //DataTable dt = GetData("SELECT reqNo FROM PeriReqNoTable WHERE userId='" + userId + "'");

                dt.Columns.Add("orderNo", typeof(int));

                foreach (DataRow dr2 in dt.Select())
                {
                    int reqLength = dr2["reqNo"].ToString().Length - 1;
                    int idx = dr2["reqNo"].ToString().LastIndexOf('-');

                    dr2["orderNo"] = Convert.ToInt16(dr2["reqNo"].ToString().Substring(idx + 1, reqLength - idx));
                }

                DataView dv = dt.DefaultView;
                dv.Sort = "orderNo desc";
                DataTable sortedDt = dv.ToTable();
                DataRow dr = sortedDt.Rows[0];
                
                newReqNo = Convert.ToInt16(dr["orderNo"]);
                newReqNo++;

                temp = ViewBag.userId + "-" + workCode + "-" + newReqNo.ToString();

            }

            else
            {
                temp = ViewBag.userId + "-" + workCode + "-" + "1";
            }

            string constr = conn;
            string errMsg = "";

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into PeriReqNoTable values (@userId, @reqNo)";

                    cmd.Parameters.AddWithValue("@userId", ViewBag.userId);
                    cmd.Parameters.AddWithValue("@reqNo", temp);
                    cmd.ExecuteNonQuery();
                }

            }
            catch(Exception e)
            {
                errMsg = e.Message;
                errMsg = "error";
            }
            finally
            {
                if(errMsg == "")
                {
                    insertApprovalData(ViewBag.userId, workCode, temp, ViewBag.personalCode, workCodeFirst, secondUser, totalValue, currentValue); // id, 근태코드, reqNo.
                }

                else
                {

                }
            }
        }
        public bool insertData(string registerId, string workCode, string userName, string personalCode, string startDate, string endDate, string reason, string viewSelLeft, string viewSelVal)
        {

            DataSet ds = getLists(registerId, personalCode, startDate, endDate);
            DataTable dt = ds.Tables[0];

            if(dt.Rows.Count == 0)
            {
                string constr = conn;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into PeriTempList values (@personalCode, @startDate, @endDate, @registerID, @workCode, @userName,  @reason, @isApproval, @gViewSelLeft, @reqNo, @gViewSelName)";

                    cmd.Parameters.AddWithValue("@personalCode", personalCode);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.Parameters.AddWithValue("@registerID", registerId);
                    cmd.Parameters.AddWithValue("@workCode", workCode);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    cmd.Parameters.AddWithValue("@reason", reason == null ? "" : reason);
                    cmd.Parameters.AddWithValue("@isApproval", "");
                    cmd.Parameters.AddWithValue("@gViewSelLeft", viewSelLeft);
                    cmd.Parameters.AddWithValue("@reqNo", "");
                    cmd.Parameters.AddWithValue("@gViewSelName", viewSelVal);
                    cmd.ExecuteNonQuery();
                }
                    return true; // 등록이 가능하면?
            }

            else
            {
                return false;
            }

        }

        private DataSet getBrowsers(string id)
        {
            string constr = conn;
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_PERI_GETBROWSERS", con);
                    cmd.Connection = con;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@WORKGB", "SELECT");
                    cmd.Parameters.AddWithValue("@userId", id);

                    SqlDataAdapter dsAdapter = new SqlDataAdapter(cmd);
                    dsAdapter.Fill(ds);
                }
                catch(Exception ex)
                {
                    Console.Write(ex.Message);
                }

                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }

                return ds;

            }
        }

        private DataSet getModalBrowser(string etc2, string userName)
        {
            string constr = conn;
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_PERI_GETBROWSERS", con);
                    cmd.Connection = con;

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@WORKGB", "SELECT2");
                    cmd.Parameters.AddWithValue("@pEtc2", etc2);
                    cmd.Parameters.AddWithValue("@pUserName", userName);

                    SqlDataAdapter dsAdapter = new SqlDataAdapter(cmd);
                    dsAdapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }

                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }

                return ds;

            }
        }

    }
}