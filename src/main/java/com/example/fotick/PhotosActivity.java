package com.example.fotick;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.widget.GridLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.SearchView;
import android.util.Log;
import android.view.View;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.EditText;

import com.example.fotick.POJO.Image;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.List;

public class PhotosActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {
    private MenuItem mSearchMenuItem;
    private SearchView mSearchView;
    EditText mText;
    String mAuthToken;
    private String mQuery;

    List<Image> mPictures;
    private String mMaxId, mMinId;
    ImageAdapter mAdapter;
    private RecyclerView mRecyclerView;
    List<String> urlstring;
    List<String> standardurl;

    private ProgressDialog mProgressDialog;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_photos);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
                        .setAction("Action", null).show();
            }
        });

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.setDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);


        Intent intent = getIntent();
        mAuthToken = intent.getStringExtra("login");

        mRecyclerView = (RecyclerView) findViewById(R.id.recyclerView);
        GridLayoutManager manager = new GridLayoutManager(this, 3);
        mRecyclerView.setLayoutManager(manager);

        showPD();
        fetchmedia();
        mAdapter = new ImageAdapter(mPictures);
        mAdapter.notifyDataSetChanged();
        mRecyclerView.setAdapter(mAdapter);


    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.photos, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_camera) {
            // Handle the camera action
        } else if (id == R.id.nav_gallery) {

        } else if (id == R.id.nav_slideshow) {

        } else if (id == R.id.nav_manage) {

        } else if (id == R.id.nav_share) {

        } else if (id == R.id.nav_send) {

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    private void showPD(){

        if(mProgressDialog==null){
            mProgressDialog = new ProgressDialog(this);
            mProgressDialog.setMessage("Loading...");
            mProgressDialog.setCancelable(false);
            mProgressDialog.setCanceledOnTouchOutside(false);
            mProgressDialog.show();
        }
    }

    private void hidePD(){
        if(mProgressDialog!= null){
            mProgressDialog.dismiss();
            mProgressDialog = null;
        }


    }
    private void fetchmedia() {


        urlstring = new ArrayList<String>();
        standardurl = new ArrayList<String>();
        mPictures = new ArrayList<Image>();
        Thread thread = new Thread() {
            @Override
            public void run() {


                try {

                    URL example = new URL("https://www.instagram.com/"
                            + mAuthToken+"/media/");
                    String perma="https://api.instagram.com/v1/users/self/media/recent?access_token="
                            + mAuthToken;
                    //while(! example.toString().equals(""))
                    Log.d(Constants.TAG,"authToken is "+mAuthToken);
                    {
                        URLConnection tc = example.openConnection();
                        BufferedReader in = new BufferedReader(new InputStreamReader(tc.getInputStream()));
                        String line;
                        String next = null;
                        while ((line = in.readLine()) != null) {
                            JSONObject ob = new JSONObject(line);


                            //  JSONObject nextobj = ob.getJSONObject("pagination");

                            JSONArray object = ob.getJSONArray("items");
                            //  	 if(! nextobj.isNull("next_max_id"))
                            //  next=nextobj.getString("next_max_id");


                            //Log.i(TAG, "pagination response "+nextobj.toString());

                            //Log.i(TAG, "next url id SSSS "+next);

                            //  JSONObject nexturlobj= ob.getJSONObject("pagination");


                            for (int i = 0; i < object.length(); i++) {


                                JSONObject jo = (JSONObject) object.get(i);
                                JSONObject nja = (JSONObject) jo.getJSONObject("images");


                                JSONObject purl3 = (JSONObject) nja
                                        .getJSONObject("thumbnail");
                                JSONObject imagelow=(JSONObject) nja
                                        .getJSONObject("low_resolution");
                                String b=purl3.getString("url");
                                String low=imagelow.getString("url");
                                urlstring.add(b);
                                standardurl.add(low);
                                Log.i(Constants.TAG, "thumbnail urls" + urlstring.get(i));

                               Image picture = new Image();
                                picture.setURL(low);
                                mPictures.add(picture);
                                Log.d(Constants.TAG,"add new picture");
                                //GridView gd=(GridView) findViewById(R.id.gridview1);
                                //gd.setAdapter(new Imageadapter(mCtx,urlstring));

                            }

                        }
                        Log.i(Constants.TAG, example.toString());

   /*  if(next!=null)
		example=new URL(perma+"&max_id="+next);
     else
    	 example=null;
	*/

                    }
                    Log.i(Constants.TAG,"total images :" +urlstring.size());
                } catch (MalformedURLException e) {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                } catch (IOException e) {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                } catch (JSONException e) {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                }



                hidePD();


            }
        };
        thread.start();
        try {
            thread.join();
        } catch (InterruptedException e) {
            Log.d(Constants.TAG,"join failed");
            e.printStackTrace();
        }

    }




}


