package pe.com.uband.uband.Fragments;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.GridView;

import com.android.volley.DefaultRetryPolicy;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

import pe.com.uband.uband.Adapters.GridImageAdapter;
import pe.com.uband.uband.R;


public class HomeFragment extends BaseVolleyFragment {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;
    GridView gridView;


    public HomeFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment HomeFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static HomeFragment newInstance(String param1, String param2) {
        HomeFragment fragment = new HomeFragment();
        Bundle args = new Bundle();
        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.fragment_home, container, false);

        gridView = (GridView) view.findViewById(R.id.gridview);

        gridView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View v,
                                    int position, long id) {
                BandDetailFragment nextFrag = BandDetailFragment.newInstance((int)id);
                FragmentManager manager = getFragmentManager();
                FragmentTransaction transaction = manager.beginTransaction();
                transaction.replace(R.id.fragment, nextFrag).addToBackStack(null); // newInstance() is a static factory method.
                transaction.commit();

            }
        });

        JsonObjectRequest bandRequest= new JsonObjectRequest(Request.Method.GET, "http://facebandapi.azurewebsites.net/api/band/get", new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {

                List<GridImageAdapter.Item> lstITems= new ArrayList<GridImageAdapter.Item>();

                try {

                    JSONArray results=response.getJSONArray("results");

                    GridImageAdapter.Item item;
                    JSONObject jsonActual;
                    for (int i=0 ; i<results.length(); i++){
                        item=new GridImageAdapter.Item();

                        jsonActual=results.getJSONObject(i);
                        item.setId(jsonActual.getInt("id"));
                        item.setNombre(jsonActual.getString("nombre"));
                        item.setDescripcion(jsonActual.getString("descripcion"));
                        item.setFoto(jsonActual.getString("foto").substring(1));
                        //item.setSeguidores(jsonActual.getInt("seguidores"));
                        lstITems.add(item);
                    }

                }catch (Exception ex){

                }

                gridView.setAdapter(new GridImageAdapter(getActivity(),lstITems));
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {

            }
        });

       /* StringRequest bandRequest=new StringRequest(Request.Method.GET, "http://facebandapi.azurewebsites.net/api/band/get", new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                gridView.setAdapter(new GridImageAdapter(getActivity()));
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {

            }
        });*/

        bandRequest.setRetryPolicy(new DefaultRetryPolicy(
                60000, 3, DefaultRetryPolicy.DEFAULT_BACKOFF_MULT
        ));

        Volley.newRequestQueue(getActivity()).add(bandRequest);
        return view;
    }

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {

    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);

    }

    @Override
    public void onDetach() {
        super.onDetach();
    }

}
