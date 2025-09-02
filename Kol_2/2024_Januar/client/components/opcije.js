import { ubaciKorisnikaUSobu } from "../utils.js";

export class Opcije {
  constructor(korisnici) {
    this.korisnici = korisnici;
  }

  draw(container) {
    const lblSoba = document.createElement("label");
    lblSoba.innerHTML = "Soba:";

    const inputSoba = document.createElement("input");
    inputSoba.type = "text";
    inputSoba.classList.add("inputSoba");

    const lblKorisnik = document.createElement("label");
    lblKorisnik.innerHTML = "Korisnik:";

    const selKorisnik = document.createElement("select");
    selKorisnik.classList.add("selKorisnik");
    this.korisnici.forEach((korisnik) => {
      let optKorisnik = document.createElement("option");
      optKorisnik.value = korisnik.id;
      optKorisnik.innerHTML = korisnik.korisnickoIme;
      selKorisnik.append(optKorisnik);
    });

    const lblNadimak = document.createElement("label");
    lblNadimak.innerHTML = "Nadimak:";

    const inputNadimak = document.createElement("input");
    inputNadimak.type = "text";
    inputNadimak.classList.add("inputNadimak");

    const lblBoja = document.createElement("label");
    lblBoja.innerHTML = "Boja:";

    const boja = document.createElement("input");
    boja.type = "color";

    const dugme = document.createElement("button");
    dugme.classList.add("dugmeDodajKorisnika");
    dugme.innerHTML = "Dodaj";
    dugme.onclick = this.dodajKorisnikaUSobu;

    container.append(
      lblSoba,
      inputSoba,
      lblKorisnik,
      selKorisnik,
      lblNadimak,
      inputNadimak,
      lblBoja,
      boja,
      dugme
    );
  }

  dodajKorisnikaUSobu = async () => {
    const imeSobe = document.querySelector(".inputSoba").value;
    const korisnik = document.querySelector(".selKorisnik").value;
    const nadimak = document.querySelector(".inputNadimak").value;
    const boja = document.querySelector('input[type="color"]').value; // hex format

    var odgovor = await ubaciKorisnikaUSobu(imeSobe, korisnik, nadimak, boja);
    if (odgovor === -1) return;

    const selKorisnik = document.querySelector(".selKorisnik");
    const tekst = selKorisnik.options[selKorisnik.selectedIndex].text;

    const clanoviDiv = document.querySelector(`.clanoviDiv-${imeSobe}`);
    const lblClan = document.createElement("label");
    lblClan.innerHTML = `- ${nadimak} (${tekst})`;
    lblClan.style.color = boja;
    clanoviDiv.append(lblClan);
  };
}
