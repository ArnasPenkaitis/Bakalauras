package com.example.bakalauras.POJO;

import android.location.Location;
import android.util.Base64;
import android.util.Log;

import com.google.android.gms.maps.model.LatLng;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.Instant;
import java.util.ArrayList;
import java.util.Date;

public class TeacherListItemPOJO {
    private final String TAG = "EVENT_LIST_ITEM_POJO";

    private String Id;
    private String Name;
    private String Surname;
    private String Email;
    private String Password;
    private String Username;

    public TeacherListItemPOJO(String id, String name, String surname, String email, String password, String username) {
        this.Id = id;
        this.Name = name;
        this.Surname = surname;
        this.Email = email;
        this.Password = password;
        this.Username = username;
    }

    public String getId() {
        return Id;
    }
    public void setId(String id) {
        Id = id;
    }

    public String getName() {
        return Name;
    }
    public void setName(String name) {
        Name = name;
    }

    public String getSurname() {
        return Surname;
    }
    public void setSurname(String surname) {
        Surname = surname;
    }

    public String getEmail() {
        return Email;
    }
    public void setEmail(String email) {
        Email = email;
    }

    public String getPassword() {
        return Password;
    }
    public void setPassword(String password) {
        Password = password;
    }

    public String getUsername() {
        return Username;
    }
    public void setUsername(String username) {
        Username = username;
    }
}
