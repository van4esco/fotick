package com.example.fotick;

import org.json.JSONArray;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

/**
 * Created by ПОДАРУНКОВИЙ on 14.05.2017.
 */
public interface PostInterface {

    @POST("users")
    Call<String> getStringScalar(@Body String body);

    @POST("images/hateinsta2")
    Call<String> getArray(@Body JSONArray body);

    @POST("images/user/sale")
    Call<String> getArraySale(@Body JSONArray body);
}
