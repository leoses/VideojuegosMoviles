package es.ucm.gdv.blas.oses.carreau.lib;

public class Fade {
    public int colorIni;
    public final int colorFin;
    public final int vel;

    public Fade(int colorIni, int colorFin, int vel) {
        this.colorIni = colorIni;
        this.colorFin = colorFin;
        this.vel = vel;
    }
}