using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonSaveAcc.actionmain
{

    public delegate void ErrorHandle(Object err, object sender, int code);
    public delegate void ProcessHandle(Object obj,object sender);
    public delegate void SuccessHandle(Object obj, object sender);
    public delegate void ActionHandle(Object obj, String data);
    public delegate void ProxyConnectHandle(Object obj, String data);

    public class ConstAll
    {
        #region Home Page
        public static String butRightHomePage = "a-link-normal feed-carousel-control feed-right";
        public static String butLeftHomePage = "a-link-normal feed-carousel-control feed-left";
        public static String xPathButRightHomePage = "//a[@class='a-link-normal feed-carousel-control feed-right']";
        #endregion

        #region Product information
        public static String smallImageProductInformation = "//*[@class='twisterSwatchWrapper_0 twisterSwatchWrapper thinWidthOverride']";
        public static String btnRightProductInformation = "//a[@class='a-button a-button-image a-carousel-button a-carousel-goto-nextpage']";
        public static String btnLeftProductInformation = "//a[@class='a-button a-button-image a-carousel-button a-carousel-goto-prevpage']";
        public static String btnReadReviewsProductInformation = "//input[@data-action='reviews:filter-action:apply']";
        public static String lbReviewComment = "//a[@alt='review image']";
        public static String lbReviewImageComment = "//a[@alt='review image']";
        public static String imgVoteUp = "//input[@value='Vote Up']";
        public static String imgVoteDown = "//input[@value='Vote Down']";
        public static String linkShowMore = "//a[@class='a-link-emphasis a-text-bold']";
        public static String btnSortComment = "//span[@class='a-declarative']/a[@class='a-link-normal a-text-normal']";
        public static String btnHelpFull = "//input[@data-hook='vote-helpful-button' and @type='submit']";
        public static String btnTypeProduct = "//img[@class='product-image']/following::a[@class='a-link-normal']";
        #endregion

        public static String btnSearchReview = "//input[@aria-labelledby='a-autoid-3-announce']";
    }
}
