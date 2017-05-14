package com.example.fotick;

import android.graphics.Color;
import android.graphics.PorterDuff;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ImageView;

import com.example.fotick.POJO.Image;
import com.squareup.picasso.Picasso;

import java.util.List;


public class ImageAdapter extends RecyclerView.Adapter<ImageAdapter.Holder>  {
    //private  ImageClickListener mlistener;

    private List<Image> mPictures;

    private List<Image> currentPictures;


    CustomItemClickListener listener;



    public ImageAdapter(List<Image> pics, CustomItemClickListener listener){
        mPictures = pics;
        this.listener = listener;

    }

    @Override
    public Holder onCreateViewHolder(ViewGroup viewGroup, final int i) {
        View row = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.grid_item, viewGroup, false);
        final Holder mViewHolder = new Holder(row);
        row.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                listener.onItemClick(v, i);
            }
        });

        return new Holder(row);
    }

    @Override
    public void onBindViewHolder(final Holder holder, int i) {
        final Image currPic = mPictures.get(i);
        Picasso.with(holder.itemView.getContext()).load(currPic.getURL()).into(holder.mPhoto1);
        holder.mPhoto1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                Log.d("AAAAAAAAAAAAAAAAAAAAAA","AAAAAAAAAAAAAAAAAAAA "+currPic.getURL());
                currPic.setChoosed(!currPic.getChoosed());
                Log.d("AAAAAAAAAAAAAAAAAAA",String.valueOf(currPic.getChoosed()));
                if(currPic.getChoosed()){
                    v.setBackgroundResource(R.drawable.shape);
                }else{
                    v.setBackgroundResource(R.drawable.none);
                }
            }
        });

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

            Log.d("test pls",String.valueOf(v.getId())) ;
            Log.d("test pls",v.toString());
            Log.d("test pls",v.getTag().toString());
        }
    }


}