package com.example.bakalauras.adapters;

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

import com.example.bakalauras.POJO.LessonListItemPOJO;
import com.example.bakalauras.R;
import com.example.bakalauras.Shared.BaseViewHolder;

import java.util.ArrayList;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;

public class lessonsAdapter extends RecyclerView.Adapter<BaseViewHolder> {
    public static final String TAG = "LESSONS_ADAPTER";
    public static final int VIEW_TYPE_EMPTY = 0;
    public static final int VIEW_TYPE_NORMAL = 1;

    public List<LessonListItemPOJO> lessonList;

    // Interface for callback
    private lessonsAdapter.LessonAdapterCallback mCallback;
    public interface LessonAdapterCallback {
        void onEmptyViewRetryClick();
    }

    public void setCallback(lessonsAdapter.LessonAdapterCallback callback)
    {
        this.mCallback = callback;
    }
    // Interface for callback

    public lessonsAdapter(){

    }



    public lessonsAdapter(ArrayList<LessonListItemPOJO> lessonList) {
        this.lessonList = lessonList;
    }

    @NonNull
    @Override
    public BaseViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        switch(viewType){
            case VIEW_TYPE_NORMAL:
                return new lessonsAdapter.ViewHolder( LayoutInflater.from(parent.getContext()).inflate( R.layout.lesson_card, parent, false) );
            case VIEW_TYPE_EMPTY:
                return new lessonsAdapter.EmptyViewHolder( LayoutInflater.from(parent.getContext()).inflate(R.layout.placeholder_unsuccessful_call, parent, false) );
            default:
                return new lessonsAdapter.EmptyViewHolder( LayoutInflater.from(parent.getContext()).inflate(R.layout.lesson_card, parent, false) );
        }
    }

    @Override
    public int getItemViewType(int position){
        if(lessonList != null && lessonList.size() > 0){
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
        if (lessonList != null && lessonList.size() > 0) {
            return lessonList.size();
        } else {
            return 1; // list is containing one element which shows that list is empty
        }
    }

    public void addItems(ArrayList<LessonListItemPOJO> lessonList){
        this.lessonList = lessonList;
        notifyDataSetChanged(); // Should be used as a last resort only because its not so efficient (notifyItemChanged for item change and notifyItemInserted for structural change
    }

    public class ViewHolder extends BaseViewHolder{


        @BindView(R.id.TV_lesson_name)
        TextView lessonName;

        @BindView(R.id.TV_lesson_abbreviation)
        TextView lessonAbbreviation;


        private @Nullable
        NavController navController;

        public ViewHolder(View itemView){
            super(itemView);
            ButterKnife.bind( this, itemView );
        }

        public void onBind(int position){
            super.onBind(position);

            final LessonListItemPOJO lesson = lessonList.get(position);


            if(lesson.getName() != null){
                lessonName.setText( lesson.getName() );
            }
            if(lesson.getAbbreviation() != null){
                lessonAbbreviation.setText( lesson.getAbbreviation() );
            }

            // Here goes item event listener
            itemView.setOnClickListener( new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    Bundle bundle = new Bundle();
                    bundle.putString("lessonId", lesson.getId());
                    Navigation.findNavController( itemView ).navigate( R.id.action_lesson_list_to_visualization_list, bundle );

                }
            } );

        }
        protected void clear(){
            lessonName.setText( "" );
            lessonAbbreviation.setText( "" );
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
