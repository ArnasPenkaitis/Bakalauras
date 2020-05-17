package com.example.bakalauras.adapters;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.recyclerview.widget.RecyclerView;

import com.example.bakalauras.ActivityModel;
import com.example.bakalauras.MainActivity;
import com.example.bakalauras.POJO.VisualizationListItemPOJO;
import com.example.bakalauras.R;
import com.example.bakalauras.Shared.BaseViewHolder;

import java.util.ArrayList;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;

public class visualizationsAdapter extends RecyclerView.Adapter<BaseViewHolder> {
    public static final String TAG = "LESSONS_ADAPTER";
    public static final int VIEW_TYPE_EMPTY = 0;
    public static final int VIEW_TYPE_NORMAL = 1;
    public List<VisualizationListItemPOJO> visualizationList;


    // Interface for callback
    private visualizationsAdapter.visualizationsAdapterCallback mCallback;
    public interface visualizationsAdapterCallback {
        void onEmptyViewRetryClick();
    }

    public void setCallback(visualizationsAdapter.visualizationsAdapterCallback callback)
    {
        this.mCallback = callback;
    }
    // Interface for callback

    public visualizationsAdapter(){

    }



    public visualizationsAdapter(ArrayList<VisualizationListItemPOJO> visualizationList) {
        this.visualizationList = visualizationList;
    }

    @NonNull
    @Override
    public BaseViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        switch(viewType){
            case VIEW_TYPE_NORMAL:
                return new visualizationsAdapter.ViewHolder( LayoutInflater.from(parent.getContext()).inflate( R.layout.visualization_card, parent, false) );
            case VIEW_TYPE_EMPTY:
                return new visualizationsAdapter.EmptyViewHolder( LayoutInflater.from(parent.getContext()).inflate(R.layout.placeholder_unsuccessful_call, parent, false) );
            default:
                return new visualizationsAdapter.EmptyViewHolder( LayoutInflater.from(parent.getContext()).inflate(R.layout.visualization_card, parent, false) );
        }
    }

    @Override
    public int getItemViewType(int position){
        if(visualizationList != null && visualizationList.size() > 0){
            return VIEW_TYPE_NORMAL;
        }
        else{
            return VIEW_TYPE_EMPTY;
        }
    }

    @Override
    public void onBindViewHolder(@NonNull BaseViewHolder holder, int position) {
        holder.onBind( position );
    }

    @Override
    public int getItemCount() {
        if (visualizationList != null && visualizationList.size() > 0) {
            return visualizationList.size();
        } else {
            return 1; // list is containing one element which shows that list is empty
        }
    }

    public void addItems(ArrayList<VisualizationListItemPOJO> visualizationList){
        this.visualizationList = visualizationList;
        notifyDataSetChanged(); // Should be used as a last resort only because its not so efficient (notifyItemChanged for item change and notifyItemInserted for structural change
    }

    public class ViewHolder extends BaseViewHolder{


        @BindView(R.id.TV_vis_name)
        TextView visualizationName;

        @BindView(R.id.TV_vis_description)
        TextView visualizationDescription;


        private @Nullable
        NavController navController;

        public ViewHolder(View itemView){
            super(itemView);
            ButterKnife.bind( this, itemView );
        }

        public void onBind(int position){
            super.onBind(position);

            final VisualizationListItemPOJO visualization = visualizationList.get(position);


            if(visualization.getName() != null){
                visualizationName.setText( visualization.getName() );
            }
            if(visualization.getDescription() != null){
                visualizationDescription.setText( visualization.getDescription() );
            }

            // Here goes item event listener
            itemView.setOnClickListener( new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    Bundle bundle = new Bundle();
                    bundle.putString("visualizationId", visualization.getId());
                    Context smth = view.getContext();
                    Intent intent= new Intent( view.getContext(), ActivityModel.class);
                    intent.putExtra("fileUrl",visualization.getFileUrl());
                    view.getContext().startActivity(intent);
                    //Navigation.findNavController( itemView ).navigate( R.id.action_visualization_list_to_visualization, bundle );

                }
            } );

        }
        protected void clear(){
            visualizationName.setText( "" );
            visualizationDescription.setText( "" );
        }
    }

    public class EmptyViewHolder extends BaseViewHolder{
        @BindView( R.id.BTN_event_retry )
        Button mBtnRetry;

        public EmptyViewHolder(View itemView){
            super(itemView);
            ButterKnife.bind( this, itemView );
        }

        @Override
        public void onBind(int position) {
            super.onBind( position );

            if(mBtnRetry != null) {
                mBtnRetry.setOnClickListener( new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        mCallback.onEmptyViewRetryClick();
                    }
                } );
            }
        }

        @Override
        protected void clear(){

        }
    }

}
