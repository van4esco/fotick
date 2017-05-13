package com.example.fotick;

import android.graphics.PorterDuff;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;

import com.example.fotick.POJO.Image;
import com.squareup.picasso.Picasso;

import java.util.List;


public class ImageAdapter extends RecyclerView.Adapter<ImageAdapter.Holder> {
    //private  ImageClickListener mlistener;

    private List<Image> mPictures;
    public ImageAdapter(List<Image> pics){
        mPictures = pics;


    }

    @Override
    public Holder onCreateViewHolder(ViewGroup viewGroup, int i) {
        View row = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.grid_item, viewGroup, false);
        return new Holder(row);
    }

    @Override
    public void onBindViewHolder(Holder holder, int i) {
        Image currPic = mPictures.get(i);
        Picasso.with(holder.itemView.getContext()).load(currPic.getURL()).into(holder.mPhoto1);

    }

    @Override
    public int getItemCount() {
        return mPictures.size();

    }

    public Image getSelectedPicture(int position) {
        return mPictures.get(position);
    }




//    public void addImage(Picture picture) {
//        mPictures.add(picture);
//    }

    public class Holder extends RecyclerView.ViewHolder implements View.OnClickListener{

        private ImageView mPhoto1, mPhoto2;
        public Holder(View itemView) {
            super(itemView);

            mPhoto1 = (ImageView)itemView.findViewById(R.id.image1);
            //mPhoto2 = (ImageView)itemView.findViewById(R.id.image2);
        }

        @Override
        public void onClick(View v) {


        }
    }


}