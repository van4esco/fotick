package com.example.fotick;

/**
 * Created by ПОДАРУНКОВИЙ on 13.05.2017.
 */
public interface OAuthDialogListener {
    public abstract void onComplete(String accessToken);
    public abstract void onError(String error);
}