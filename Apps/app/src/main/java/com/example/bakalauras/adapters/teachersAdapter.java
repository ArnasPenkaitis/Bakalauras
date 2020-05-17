package com.example.bakalauras.adapters;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.ViewGroup;

import com.example.bakalauras.Shared.BaseViewHolder;
import com.example.bakalauras.POJO.TeacherListItemPOJO;
import com.example.bakalauras.R;

import java.util.ArrayList;
import java.util.List;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.navigation.Navigation;
import androidx.recyclerview.widget.ItemTouchHelper;
import androidx.recyclerview.widget.RecyclerView;

import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import butterknife.BindView;
import butterknife.ButterKnife;
import com.bumptech.glide.Glide;
import androidx.navigation.NavController;


public class teachersAdapter extends RecyclerView.Adapter<BaseViewHolder> {
    public static final String TAG = "TeachersAdapter";
    public static final int VIEW_TYPE_EMPTY = 0;
    public static final int VIEW_TYPE_NORMAL = 1;

    public List<TeacherListItemPOJO>  teacherList;

    // Interface for callback
    private TeacherAdapterCallback mCallback;
    public interface TeacherAdapterCallback {
        void onEmptyViewRetryClick();
    }

    public void setCallback(TeacherAdapterCallback callback)
    {
        this.mCallback = callback;
    }
    // Interface for callback

    public teachersAdapter(){

    }



    public teachersAdapter(ArrayList<TeacherListItemPOJO> teacherList) {
        this.teacherList = teacherList;
    }

    @NonNull
    @Override
    public BaseViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        switch(viewType){
            case VIEW_TYPE_NORMAL:
                return new ViewHolder( LayoutInflater.from(parent.getContext()).inflate( R.layout.teacher_card, parent, false) );
            case VIEW_TYPE_EMPTY:
                return new EmptyViewHolder( LayoutInflater.from(parent.getContext()).inflate(R.layout.placeholder_unsuccessful_call, parent, false) );
            default:
                return new EmptyViewHolder( LayoutInflater.from(parent.getContext()).inflate(R.layout.teacher_card, parent, false) );
        }
    }

    @Override
    public int getItemViewType(int position){
        if(teacherList != null && teacherList.size() > 0){
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
        if (teacherList != null && teacherList.size() > 0) {
            return teacherList.size();
        } else {
            return 1; // list is containing one element which shows that list is empty
        }
    }

    public void addItems(ArrayList<TeacherListItemPOJO> teacherList){
        this.teacherList = teacherList;
        notifyDataSetChanged(); // Should be used as a last resort only because its not so efficient (notifyItemChanged for item change and notifyItemInserted for structural change
    }

    public class ViewHolder extends BaseViewHolder{


        @BindView(R.id.TV_teacher_name)
        TextView teacherName;


        private @Nullable
        NavController navController;

        public ViewHolder(View itemView){
            super(itemView);
            ButterKnife.bind( this, itemView );
        }

        public void onBind(int position){
            super.onBind(position);

            final TeacherListItemPOJO teacher = teacherList.get(position);


            if(teacher.getName() != null){
                teacherName.setText( teacher.getName() + " " + teacher.getSurname() );
            }

            // Here goes item event listener
            itemView.setOnClickListener( new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    Bundle bundle = new Bundle();
                    bundle.putString("teacherId", teacher.getId());
                    Navigation.findNavController( itemView ).navigate( R.id.action_teacher_list_to_subjects, bundle );

                }
            } );

        }
        protected void clear(){
            teacherName.setText( "" );
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
