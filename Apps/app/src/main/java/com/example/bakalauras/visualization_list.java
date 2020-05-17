package com.example.bakalauras;

import android.content.Context;
import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.constraintlayout.widget.ConstraintLayout;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.DefaultItemAnimator;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

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

import com.example.bakalauras.POJO.VisualizationListItemPOJO;
import com.example.bakalauras.Shared.APIRequest;
import com.example.bakalauras.Shared.AppConf;
import com.example.bakalauras.Shared.MainThreadOkHttpCallback;
import com.example.bakalauras.adapters.visualizationsAdapter;
import com.google.android.material.floatingactionbutton.FloatingActionButton;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import okhttp3.OkHttpClient;
import okhttp3.Request;


/**
 * A simple {@link Fragment} subclass.
 */
public class visualization_list extends Fragment
        implements com.example.bakalauras.adapters.visualizationsAdapter.visualizationsAdapterCallback {
    private final String TAG = "VisualizationListFragment";
    private final int UPCOMING_EVENTS = 0;
    private final int FINISHED_EVENTS = 1;

    // application classes
    private APIRequest apiRequest;

    // framework components
    private LinearLayoutManager linearLayoutManager;
    private OkHttpClient okHttpClient;

    // layout vars
    private com.example.bakalauras.adapters.visualizationsAdapter visualizationsAdapter;
    private RecyclerView recyclerView;
    private View mThisFragmentView;
    private ArrayList<String> visualizationsNames;
    private ArrayList<VisualizationListItemPOJO> visualizationsList;
    private AlphaAnimation inAlphaAnimation;
    private AlphaAnimation outAlphaAnimation;
    private FrameLayout progressBarHolder;
    private ConstraintLayout mSearchLayout;
    private FloatingActionButton mFABSearch;
    private ImageButton mActionSearchClose;
    private ImageButton mActionSearchContent;
    private AutoCompleteTextView mACTVSearch;

    private String lessonId;

    public visualization_list(){
        // Required empty constructor
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        Bundle bundle = this.getArguments();
        if (bundle != null) {
            lessonId = bundle.getString("lessonId");
        }
        // Inflate the layout for this fragment
        return inflater.inflate( R.layout.fragment_visualization_list, container, false );
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

        visualizationsAdapter = new visualizationsAdapter(  );
        visualizationsAdapter.addItems( visualizationsList );
        visualizationsAdapter.setCallback( this );
        recyclerView.setAdapter( visualizationsAdapter );

        fetchDataForAdapter();

    }

    private void fetchDataForAdapter(){
        try {
            progressBarHolder.setVisibility( View.VISIBLE );
            getVisualizationsListFromApi();
        }
        catch (Exception e){
            Log.e( "fetchDataForAdapter", "getEventListData api call failed " + e );
        }
    }

    private void getVisualizationsListFromApi() throws Exception {
        okHttpClient = apiRequest.getOkHttpClientObject( 5 );
//pakeisti route
        AppConf apiConf = AppConf.getInstance();
        String lessonsListApiRoute = apiConf.getVisualizationsGetListApiRoute(lessonId);

        //route
        Request request = apiRequest.getRequestObject( lessonsListApiRoute, false, false, "", null );

        okHttpClient.newCall(request).enqueue( new MainThreadOkHttpCallback() {

            @Override
            public void apiCallSuccess(String body){
                try{
                    JSONArray responseRoot = new JSONArray( body );

                    for(int i = 0; i < responseRoot.length(); i++){

                        JSONObject jObj = responseRoot.getJSONObject( i );
                        VisualizationListItemPOJO visualizationListItemPOJO = new VisualizationListItemPOJO(
                                jObj.getString( "id" ),
                                jObj.getString("name"),
                                jObj.getString( "description" ),
                                jObj.getString( "fileUrl" )
                                );
                        visualizationsList.add( visualizationListItemPOJO );
                        visualizationsNames.add(visualizationListItemPOJO.getName());
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
        recyclerView = view.findViewById( R.id.RVL_visual_list );
        visualizationsList = new ArrayList<>(  );
        visualizationsNames = new ArrayList<>(  );
        mSearchLayout = view.findViewById( R.id.CL_events_list_search );
        mFABSearch = view.findViewById( R.id.FAB_lesson_list2_search );
        mActionSearchClose = view.findViewById( R.id.IV_event_list_action_close );
        mActionSearchContent = view.findViewById( R.id.IV_event_list_action_search );
        mACTVSearch = view.findViewById( R.id.ACTV_events_list_search );
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

        ArrayAdapter<String> ACTVAdapter = new ArrayAdapter<String>( getContext(), android.R.layout.simple_list_item_1, visualizationsNames );
        mACTVSearch.setAdapter( ACTVAdapter );


        mActionSearchContent.setOnClickListener( new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ArrayList<VisualizationListItemPOJO> filteredVisualizations = filterEventsByName( mACTVSearch.getText().toString() );
                visualizationsAdapter.addItems( filteredVisualizations );
                recyclerView.setAdapter( visualizationsAdapter );

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
    private ArrayList<VisualizationListItemPOJO> filterEventsByName(String name){
        ArrayList<VisualizationListItemPOJO> sortedEvents = new ArrayList<>(  );

        for(VisualizationListItemPOJO event : visualizationsList){
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