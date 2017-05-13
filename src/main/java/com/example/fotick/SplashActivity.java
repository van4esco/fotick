package com.example.fotick;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.graphics.Typeface;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.os.Handler;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

/**
 * An example full-screen activity that shows and hides the system UI (i.e.
 * status bar and navigation/system bar) with user interaction.
 */
public class SplashActivity extends AppCompatActivity {
    TextView logoTextView;
    TextView bottomLogoTextView;
    EditText login;
    Button button;

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
        login = (EditText)findViewById(R.id.editText);
        button = (Button)findViewById(R.id.button);

    }


    public void onClick(View view) {
        Intent intent = new Intent(SplashActivity.this, PhotosActivity.class);
        intent.putExtra("login",login.getText().toString());
        startActivity(intent);
    }
}
