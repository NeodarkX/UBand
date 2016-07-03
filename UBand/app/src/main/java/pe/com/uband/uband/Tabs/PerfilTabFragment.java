package pe.com.uband.uband.Tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.android.volley.DefaultRetryPolicy;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;
import com.squareup.picasso.Picasso;

import org.json.JSONObject;

import de.hdodenhof.circleimageview.CircleImageView;
import pe.com.uband.uband.Fragments.BaseVolleyFragment;
import pe.com.uband.uband.R;

public class PerfilTabFragment extends BaseVolleyFragment {
    public PerfilTabFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment PerfilTabFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static PerfilTabFragment newInstance(String param1, String param2) {
        PerfilTabFragment fragment = new PerfilTabFragment();
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
        View rootView = inflater.inflate(R.layout.fragment_perfil_tab, container, false);
        final TextView txtNombre=(TextView)rootView.findViewById(R.id.Nombre);
        final TextView txtDescripcion=(TextView)rootView.findViewById(R.id.Descripcion);
        final CircleImageView imgPerfil=(CircleImageView)rootView.findViewById(R.id.profile_image);


        Bundle bundle= getArguments();
        Integer idband=bundle.getInt("bandid");

        JsonObjectRequest itemBandRequest= new JsonObjectRequest(Request.Method.GET, "http://facebandapi.azurewebsites.net/api/band/detail/"+idband, new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                try {

                    response=response.getJSONObject("results");
                    txtNombre.setText(response.getString("nombre"));
                    txtDescripcion.setText(response.getString("descripcion"));
                    Picasso.with(getActivity()).load("http://faceband.azurewebsites.net"+response.getString("foto").substring(1)).into(imgPerfil);

                }catch (Exception ex){

                }

            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {

            }
        });

        itemBandRequest.setRetryPolicy(new DefaultRetryPolicy(
                60000, 3, DefaultRetryPolicy.DEFAULT_BACKOFF_MULT
        ));

        Volley.newRequestQueue(getActivity()).add(itemBandRequest);
        return rootView;
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
