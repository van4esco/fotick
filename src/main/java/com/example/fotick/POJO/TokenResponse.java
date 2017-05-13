package com.example.fotick.POJO;

/**
 * Created by ПОДАРУНКОВИЙ on 13.05.2017.
 */

public class TokenResponse {
    User user;



    String acsess_token;


    @Override
    public String toString() {
        return "TokenResponse{" +
                "user=" + user +
                ", acsess_token='" + acsess_token + '\'' +
                '}';
    }

    public User getUser() {
        return user;
    }

    public void setUser(User user) {
        this.user = user;
    }

    public String getAcsess_token() {
        return acsess_token;
    }

    public void setAcsess_token(String acsess_token) {
        this.acsess_token = acsess_token;
    }

}
