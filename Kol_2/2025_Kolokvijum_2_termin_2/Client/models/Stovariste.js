export class Stovariste {
  constructor(id, naziv, adresa, brojTelefona, materijaliUMagacinu = []) {
    this.id = id;
    this.naziv = naziv;
    this.adresa = adresa;
    this.brojTelefona = brojTelefona;
    this.materijaliUMagacinu = materijaliUMagacinu;
  }
}
