package com.example.fotick;

import android.app.Application;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.util.Log;
import android.view.Display;
import android.view.ViewGroup;
import android.view.Window;
import android.webkit.CookieManager;
import android.webkit.CookieSyncManager;
import android.webkit.WebResourceError;
import android.webkit.WebResourceRequest;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.Spinner;

import static android.R.attr.width;
import static com.example.fotick.R.attr.height;

/**
 * Created by ПОДАРУНКОВИЙ on 13.05.2017.
 */

public class LoginDialogImp extends Dialog {

    private static final String TAG = Constants.TAG;
    private static final float[] DIMENSIONS_PORTRAIT = {320, 460};
    private static final float[] DIMENSIONS_LANDSCAPE = {480,300};
    private OAuthDialogListener mListener;
    private WebView webView;
    private String dialogURL = "https://api.instagram.com/oauth/authorize/?client_id=" + Constants.CLIENT_ID + "&redirect_uri=" + Constants.REDIRECT_URI + "&response_type=code&display=touch&scope=public_content";
    ProgressDialog mSpinner;
    LinearLayout mContent;
    Context context;


    private static LoginDialogImp instance;



    public static synchronized LoginDialogImp getInstance(Context context) {
        if (instance == null) {
            instance = new LoginDialogImp(context);
        }
        return instance;
    }

    public LoginDialogImp(@NonNull Context context) {
        super(context);
        this.context = context;

    }




    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Log.d("LOG_INST","authDialog go to creating");
        mSpinner = new ProgressDialog(getContext());
        mSpinner.requestWindowFeature(Window.FEATURE_NO_TITLE);
        mSpinner.setMessage("Loading...");

        mContent = new LinearLayout(getContext());
        mContent.setOrientation(LinearLayout.VERTICAL);

        requestWindowFeature(Window.FEATURE_NO_TITLE);

        setUpWebView();


        Display display = getWindow().getWindowManager().getDefaultDisplay();
        final float scale = getContext().getResources().getDisplayMetrics().density;
        float[] dimensions = (display.getWidth() < display.getHeight()) ? DIMENSIONS_PORTRAIT
                : DIMENSIONS_LANDSCAPE;

        addContentView(mContent, new FrameLayout.LayoutParams(
                (int) (dimensions[0] * scale + 0.5f), (int) (dimensions[1]
                * scale + 0.5f)));

        CookieSyncManager.createInstance(getContext());
        CookieManager cookieManager = CookieManager.getInstance();
        cookieManager.removeAllCookie();
        Log.d("LOG_INST","authDialog end created");
    }

    private void setUpWebView() {
        webView = new WebView(getContext());
        webView.setVerticalScrollBarEnabled(false);
        webView.setHorizontalScrollBarEnabled(false);
        webView.setWebViewClient(new OAuthWebViewClient());
        webView.getSettings().setJavaScriptEnabled(true);
        webView.loadUrl(dialogURL);
        webView.setLayoutParams(new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.MATCH_PARENT));
        mContent.addView(webView);
    }

    private class OAuthWebViewClient extends WebViewClient {

        @Override
        public boolean shouldOverrideUrlLoading(WebView view, String url) {
            Log.d(TAG, "Redirecting URL " + url);

            if (url.startsWith(Constants.REDIRECT_URI)) {
                String urls[] = url.split("=");
                mListener.onComplete(urls[1]);


                //mListener.onComplete(urls[1]);
                // InstagramDialog.this.dismiss();
                return true;
            }


            return false;
        }

        @Override
        public void onReceivedError(WebView view, int errorCode,
                                    String description, String failingUrl) {
            Log.d(TAG, "Page error: " + description);

            super.onReceivedError(view, errorCode, description, failingUrl);
//            mListener.onError(description);
//            InstagramDialog.this.dismiss();
            Log.d("LOG_INST","onReceivedError");
        }

        @Override
        public void onPageStarted(WebView view, String url, Bitmap favicon) {
            Log.d(TAG, "Loading URL: " + url);

            super.onPageStarted(view, url, favicon);
            mSpinner.show();
        }

        @Override
        public void onPageFinished(WebView view, String url) {
            super.onPageFinished(view, url);
            String title = webView.getTitle();
//            if (title != null && title.length() > 0) {
//                mTitle.setText(title);
//         }
            Log.d(TAG, "onPageFinished URL: " + url);
            mSpinner.dismiss();
        }

    }

}
