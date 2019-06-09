package com.kodesiana.kflearning.api;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface KodesianaService {
    @GET("posts")
    Call<List<Post>> posts(@Path("user") String user);
}
