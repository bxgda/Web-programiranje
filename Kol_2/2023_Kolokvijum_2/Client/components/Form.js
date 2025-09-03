import { cekiraniPredmeti, studenti } from '../main.js';
import { Ocena } from '../models/Ocena.js';
import { Predmet } from '../models/Predmet.js';
import { Student } from '../models/Student.js';
import { dodajOcenu, dodajPredmetCheckbox, drawStudenti, getRandomColor } from '../utils.js';

export class Form {
  constructor() {
    this.fields = [
      {
        label: 'Broj indeksa:',
        type: 'number',
        class: 'input-broj-indeksa',
      },
      {
        label: 'Ime i prezime:',
        type: 'text',
        class: 'input-ime-prezime',
      },
      {
        label: 'Predmet:',
        type: 'text',
        class: 'input-predmet',
      },
      {
        label: 'Ocena:',
        type: 'number',
        class: 'input-ocena',
      },
    ];
  }

  draw(container) {
    this.fields.forEach((f) => {
      const lbl = document.createElement('label');
      lbl.innerText = f.label;

      const input = document.createElement('input');
      input.type = f.type;
      input.className = f.class;

      container.append(lbl, input);
    });

    const btn = document.createElement('button');
    btn.className = 'btn-upisi';
    btn.onclick = this.handleSubmit;
    btn.innerText = 'Upisi';

    container.append(btn);
  }

  handleSubmit = async () => {
    const brIndInput = document.querySelector('.input-broj-indeksa');
    const imePrezimeInput = document.querySelector('.input-ime-prezime');
    const predmetInput = document.querySelector('.input-predmet');
    const ocenaInput = document.querySelector('.input-ocena');

    const data = await dodajOcenu(
      brIndInput.value,
      imePrezimeInput.value,
      predmetInput.value,
      ocenaInput.value
    );

    if (data) {
      // Osvezi checkbox-ove
      const predmetCheckbox = document.querySelector(`.checkbox-${data.predmet.id}`);
      if (predmetCheckbox) {
        predmetCheckbox.checked = true;
      } else {
        const predmet = new Predmet(data.predmet.id, data.predmet.naziv, getRandomColor());

        // dodaj u global state za predmete
        cekiraniPredmeti.push(predmet);

        const predmetiContainer = document.querySelector('.predmeti-container');
        dodajPredmetCheckbox(predmet, predmetiContainer, true);
      }

      // Osvesi studente
      const student = studenti.find((s) => s.id === data.student.id);
      const ocena = new Ocena(data.id, data.vrednost, data.predmet.id, data.predmet.naziv);

      if (!student) {
        studenti.push(
          new Student(data.student.id, data.student.indeks, data.student.imePrezime, [ocena])
        );
      } else {
        student.dodajOcenu(ocena);
      }

      drawStudenti();
    }

    // Reset polja
    brIndInput.value = '';
    imePrezimeInput.value = '';
    predmetInput.value = '';
    ocenaInput.value = '';
  };
}
