package com.example.fotick.POJO;

/**
 * Created by ПОДАРУНКОВИЙ on 13.05.2017.
 */

public class Image {
    String URL;
    Boolean choosed=false;

    public Boolean getChoosed() {
        return choosed;
    }

    public void setChoosed(Boolean choosed) {
        this.choosed = choosed;
    }

    public String getURL() {
        return URL;
    }

    public void setURL(String URL) {
        this.URL = URL;
    }
}
