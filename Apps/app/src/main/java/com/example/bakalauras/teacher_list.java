package com.example.bakalauras;

import android.content.Context;
import android.media.Image;
import android.os.Bundle;

import androidx.annotation.Nullable;
import androidx.constraintlayout.widget.ConstraintLayout;
import androidx.fragment.app.Fragment;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.AlphaAnimation;
import android.view.inputmethod.InputMethodManager;
import android.widget.ArrayAdapter;
import android.widget.AutoCompleteTextView;
import android.widget.FrameLayout;
import android.widget.ImageButton;
import android.widget.ImageView;

import com.example.bakalauras.adapters.teachersAdapter;
import com.example.bakalauras.POJO.TeacherListItemPOJO;
import com.example.bakalauras.R;
import com.example.bakalauras.Shared.APIRequest;
import com.example.bakalauras.Shared.AppConf;
import com.example.bakalauras.Shared.CacheInterceptor;
import com.example.bakalauras.Shared.MainThreadOkHttpCallback;
import com.google.android.material.floatingactionbutton.FloatingActionButton;
import com.google.android.material.tabs.TabLayout;

import java.io.File;
import java.util.ArrayList;
import java.util.Date;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.DefaultItemAnimator;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

// With HTTP calls related libs
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import okhttp3.Cache;
import okhttp3.HttpUrl;
import okhttp3.OkHttpClient;
import okhttp3.Request;


/**
 * A simple {@link Fragment} subclass.
 */
public class teacher_list extends Fragment
        implements com.example.bakalauras.adapters.teachersAdapter.TeacherAdapterCallback {
    private final String TAG = "TeacherListFragment";
    private final int UPCOMING_EVENTS = 0;
    private final int FINISHED_EVENTS = 1;

    // application classes
    private APIRequest apiRequest;

    // framework components
    private LinearLayoutManager linearLayoutManager;
    private OkHttpClient okHttpClient;

    // layout vars
    private teachersAdapter teachersAdapter;
    private RecyclerView recyclerView;
    private TabLayout mTabLayout;
    private View mThisFragmentView;
    private ArrayList<String> teachersNames;
    private ArrayList<TeacherListItemPOJO> teachersList;
    private AlphaAnimation inAlphaAnimation;
    private AlphaAnimation outAlphaAnimation;
    private FrameLayout progressBarHolder;
    private ConstraintLayout mSearchLayout;
    private FloatingActionButton mFABSearch;
    private ImageButton mActionSearchClose;
    private ImageButton mActionSearchContent;
    private AutoCompleteTextView mACTVSearch;

    public teacher_list(){
        // Required empty constructor
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate( R.layout.fragment_teacher_list, container, false );
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated( view, savedInstanceState );
        mThisFragmentView = view;

        bindViews( view );
        prepareListeners( view );
        setupRecyclerView();
    }

    private void setupRecyclerView(){

        linearLayoutManager = new LinearLayoutManager( this.getContext() );
        linearLayoutManager.setOrientation( RecyclerView.VERTICAL );
        recyclerView.setLayoutManager( linearLayoutManager );
        recyclerView.setItemAnimator( new DefaultItemAnimator() );

        teachersAdapter = new teachersAdapter(  );
        teachersAdapter.addItems( teachersList );
        teachersAdapter.setCallback( this );
        recyclerView.setAdapter( teachersAdapter );

        fetchDataForAdapter();

    }

    private void fetchDataForAdapter(){
        try {
            progressBarHolder.setVisibility( View.VISIBLE );
            getTeachersListFromApi();
        }
        catch (Exception e){
            Log.e( "fetchDataForAdapter", "getEventListData api call failed " + e );
        }
    }

    private void getTeachersListFromApi() throws Exception {
        okHttpClient = apiRequest.getOkHttpClientObject( 5 );

        AppConf apiConf = AppConf.getInstance();
        String teachersListApiRoute = apiConf.getTeacherGetListApiRoute();

        //route
        Request request = apiRequest.getRequestObject( teachersListApiRoute, false, false, "", null );

        okHttpClient.newCall(request).enqueue( new MainThreadOkHttpCallback() {

            @Override
            public void apiCallSuccess(String body){
                try{
                    JSONArray responseRoot = new JSONArray( body );

                    for(int i = 0; i < responseRoot.length(); i++){

                        JSONObject jObj = responseRoot.getJSONObject( i );
                        TeacherListItemPOJO teacherListItemPOJO = new TeacherListItemPOJO(
                                jObj.getString( "id" ),
                                jObj.getString("name"),
                                jObj.getString( "surname" ),
                                jObj.getString( "email" ),
                                jObj.getString( "username" ),
                                jObj.getString( "password" )
                        );
                        teachersList.add( teacherListItemPOJO );
                        teachersNames.add(teacherListItemPOJO.getName());
                    }
                    progressBarHolder.setVisibility( View.GONE );
                }
                catch (JSONException e){
                    Log.e("OkHttp", "Error while parsing api/event response data - " + e);
                }
            }

            @Override
            public void apiCallFail(Exception e){
                progressBarHolder.setVisibility( View.GONE );
                Log.e("OkHttp", "Api call http://<host>/api/event failed");
            }

        } );

    }

    private void bindViews(View view){
        progressBarHolder = view.findViewById( R.id.FL_PB_holder_events_list );
        recyclerView = view.findViewById( R.id.RVL_events_list );
        teachersList = new ArrayList<>(  );
        teachersNames = new ArrayList<>(  );
        mSearchLayout = view.findViewById( R.id.CL_events_list_search );
        mFABSearch = view.findViewById( R.id.FAB_events_list_search );
        mActionSearchClose = view.findViewById( R.id.IV_event_list_action_close );
        mActionSearchContent = view.findViewById( R.id.IV_event_list_action_search );
        mACTVSearch = view.findViewById( R.id.ACTV_events_list_search );
        //mTabLayout = view.findViewById( R.id.TBL_events_tab_layout );
        apiRequest = new APIRequest( getContext() );
    }

    private void prepareListeners(View view){

        mFABSearch.setOnClickListener( new View.OnClickListener(){
            @Override
            public void onClick(View v){
                mSearchLayout.setVisibility( View.VISIBLE );

            }
        });
        mActionSearchClose.setOnClickListener( new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                mSearchLayout.setVisibility( View.GONE );
            }
        } );

        ArrayAdapter<String> ACTVAdapter = new ArrayAdapter<String>( getContext(), android.R.layout.simple_list_item_1, teachersNames );
        mACTVSearch.setAdapter( ACTVAdapter );


        mActionSearchContent.setOnClickListener( new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ArrayList<TeacherListItemPOJO> filteredEvents = filterEventsByName( mACTVSearch.getText().toString() );
                teachersAdapter.addItems( filteredEvents );
                recyclerView.setAdapter( teachersAdapter );

                InputMethodManager inputMethodManager = (InputMethodManager) getContext().getSystemService( Context.INPUT_METHOD_SERVICE );
                try {
                    inputMethodManager.hideSoftInputFromWindow( mThisFragmentView.getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS );
                }
                catch (NullPointerException e){
                    Log.e(TAG, e.toString());
                }
                mActionSearchClose.callOnClick();
            }
        } );
    }


    /**
     *
     * @param name (String) Event name
     * @return ArrayList<EventListItemPOJO> list
     */
    private ArrayList<TeacherListItemPOJO> filterEventsByName(String name){
        ArrayList<TeacherListItemPOJO> sortedEvents = new ArrayList<>(  );

        for(TeacherListItemPOJO event : teachersList){
            if(event.getName().toLowerCase().contains( name.toLowerCase() )){
                sortedEvents.add( event );
            }
        }

        return sortedEvents;
    }


    public void onEmptyViewRetryClick() {
        fetchDataForAdapter();
    }
}