package com.example.fotick;

import android.annotation.SuppressLint;
import android.graphics.Typeface;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.os.Handler;
import android.view.MotionEvent;
import android.view.View;
import android.widget.TextView;

/**
 * An example full-screen activity that shows and hides the system UI (i.e.
 * status bar and navigation/system bar) with user interaction.
 */
public class SplashActivity extends AppCompatActivity {
    TextView logoTextView;
    TextView bottomLogoTextView;
    private final View.OnTouchListener mDelayHideTouchListener = new View.OnTouchListener() {
        @Override
        public boolean onTouch(View view, MotionEvent motionEvent) {

            return false;
        }
    };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_splash);


        Typeface myTypeface = Typeface.createFromAsset(getAssets(), "fonts/sweetsensations.ttf");
        logoTextView = (TextView)findViewById(R.id.fullscreen_content);
        logoTextView.setTypeface(myTypeface);
        Typeface myTypeface2 = Typeface.createFromAsset(getAssets(), "fonts/robotoregular.ttf");
        bottomLogoTextView = (TextView)findViewById(R.id.fullscreenbottom_content);
        bottomLogoTextView.setTypeface(myTypeface2);
    }

}
