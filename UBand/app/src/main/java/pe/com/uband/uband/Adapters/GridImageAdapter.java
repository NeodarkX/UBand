package pe.com.uband.uband.Adapters;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.squareup.picasso.Picasso;

import java.util.List;

import pe.com.uband.uband.R;

/**
 * Created by root on 19/06/16.
 */
public class GridImageAdapter extends BaseAdapter {
    private  List<Item> mItems;
    private  LayoutInflater mInflater;
    private Context context;

    public GridImageAdapter(Context context,List<Item> mItems){
        this.context=context;
        mInflater = LayoutInflater.from(context);
        this.mItems=mItems;
    }

    /*public void exampleAdd(){
        mItems.add(new Item("Pop", R.drawable.pop));

        mItems.add(new Item("Rock",R.drawable.rock));

        mItems.add(new Item("Rock",R.drawable.rock));

        mItems.add(new Item("Rock",R.drawable.rock));

        mItems.add(new Item("Rock",R.drawable.rock));
    }*/


    @Override
    public int getCount() {
        return mItems.size();
    }

    @Override
    public Item getItem(int position) {
        return mItems.get(position);
    }

    @Override
    public long getItemId(int position) {
        return mItems.get(position).id;
    }

    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {
        View v = view;
        ImageView picture;
        TextView name;

        if (v == null) {
            v = mInflater.inflate(R.layout.gridimage_item, viewGroup, false);
            v.setTag(R.id.picture, v.findViewById(R.id.picture));
            v.setTag(R.id.text, v.findViewById(R.id.text));
        }

        picture = (ImageView) v.getTag(R.id.picture);
        name = (TextView) v.getTag(R.id.text);

        Item item = (Item) getItem(i);

        Picasso.with(context).load("http://faceband.azurewebsites.net"+getItem(i).getFoto()).into(picture);
        //picture.setImageResource(item.drawableId);
        name.setText(item.nombre);

        return v;
    }

    public static class Item {
        private Integer id;
        private String nombre;
        private String descripcion;
        private String foto;
        private Integer seguidores;

        public Item() {
        }

        public Item(Integer id, String nombre, String foto, String descripcion, Integer seguidores) {
            this.id = id;
            this.nombre = nombre;
            this.foto = foto;
            this.descripcion = descripcion;
            this.seguidores = seguidores;
        }

        public Integer getId() {
            return id;
        }

        public void setId(Integer id) {
            this.id = id;
        }

        public String getDescripcion() {
            return descripcion;
        }

        public void setDescripcion(String descripcion) {
            this.descripcion = descripcion;
        }

        public String getNombre() {
            return nombre;
        }

        public void setNombre(String nombre) {
            this.nombre = nombre;
        }

        public Integer getSeguidores() {
            return seguidores;
        }

        public void setSeguidores(Integer seguidores) {
            this.seguidores = seguidores;
        }

        public String getFoto() {
            return foto;
        }

        public void setFoto(String foto) {
            this.foto = foto;
        }
    }
}
