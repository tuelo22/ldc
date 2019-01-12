package br.com.listadecompras.listadecompras.Util;

import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.widget.EditText;

import java.lang.ref.WeakReference;
import java.math.BigDecimal;
import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.util.Locale;

/**
 * Created by Julio on 02/12/2017.
 */

public class QuantidadeTextWatcher  implements TextWatcher {
    private final WeakReference<EditText> editTextWeakReference;
    private final Locale locale;
    private String current = "";

    public QuantidadeTextWatcher(EditText editText, Locale locale) {
        this.editTextWeakReference = new WeakReference<EditText>(editText);
        this.locale = locale != null ? locale : Locale.getDefault();
    }

    public QuantidadeTextWatcher(EditText editText) {
        this.editTextWeakReference = new WeakReference<EditText>(editText);
        this.locale = Locale.getDefault();
    }

    @Override
    public void beforeTextChanged(CharSequence s, int start, int count, int after) {

    }

    @Override
    public void onTextChanged(CharSequence s, int start, int before, int count) {

    }

    @Override
    public void afterTextChanged(Editable editable) {
        EditText editText = editTextWeakReference.get();

        if (!editable.toString().equals(current)) {
            String numero = editable.toString().replace(".", "").replace(",","");

            BigDecimal valor = new BigDecimal(numero).divide(BigDecimal.valueOf(1000));

            DecimalFormat format = new DecimalFormat("0.000");
            String formatted = format.format(valor);
            current = formatted;

            editText.setText(formatted);
            editText.setSelection(formatted.length());
        }
    }
}
