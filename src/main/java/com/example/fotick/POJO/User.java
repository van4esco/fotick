package com.example.fotick.POJO;

/**
 * Created by ПОДАРУНКОВИЙ on 13.05.2017.
 */

public class User {
    public String id;



    public String profile_picture;
    public String username;
    public String fullname;


    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getProfile_picture() {
        return profile_picture;
    }

    public void setProfile_picture(String profile_picture) {
        this.profile_picture = profile_picture;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getFullname() {
        return fullname;
    }

    public void setFullname(String fullname) {
        this.fullname = fullname;
    }

    @Override
    public String toString() {
        return "User{" +
                "id='" + id + '\'' +
                ", profile_picture='" + profile_picture + '\'' +
                ", username='" + username + '\'' +
                ", fullname='" + fullname + '\'' +
                '}';
    }
}
