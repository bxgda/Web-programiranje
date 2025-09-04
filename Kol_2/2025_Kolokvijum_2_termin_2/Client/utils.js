import { Materijal } from "./models/Materijal.js";
import { Stovariste } from "./models/Stovariste.js";

const baseUrl = "http://localhost:5043/Stovariste";

export async function preuzmiStovarista() {
  try {
    const response = await fetch(`${baseUrl}/PreuzmiStovarista`);
    const data = await response.json();

    const stovarista = data.map((s) => {
      return new Stovariste(s.id, s.naziv, s.adresa, s.brojTelefona);
    });

    return stovarista;
  } catch (error) {
    console.error(error);
    return [];
  }
}

// POST {{StovaristeServer_HostAddress}}/Stovariste/DodavanjeMaterijala/1/19000/130
// Content-Type: application/json
// Accept: application/json

// {
//     "Naziv": "Staklo",
//     "Datum": "2025-02-01T08:00:00.000Z"
// }

// [HttpPost("DodavanjeMaterijala/{stovaristeID}/{kolicina}/{cena}")]

export async function dodajMaterijal(
  stovaristeID,
  koilicina,
  cena,
  naziv,
  datum
) {
  const obj = {
    naziv: naziv,
    datum: datum,
  };

  try {
    const response = await fetch(
      `${baseUrl}/DodavanjeMaterijala/${stovaristeID}/${koilicina}/${cena}`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(obj),
      }
    );

    if (!response.ok) {
      const err = await response.text();
      return {
        error: err,
        data: null,
      };
    }

    const data = await response.json();
    return {
      error: null,
      data: data,
    };
  } catch (error) {
    console.error(error);
    return {
      error: "Greska pri dodavanju materijala",
      data: null,
    };
  }
}

// [
//     {
//       "id": 2,
//       "materijal": {
//         "id": 2,
//         "naziv": "Cement",
//         "datum": "2025-04-17T13:49:28.7863066+02:00",
//         "magaciniSaMaterijalom": [
//           null
//         ]
//       },
//       "stovariste": null,
//       "cena": 160,
//       "kolicina": 10200
//     }
//   ]

// GET {{StovaristeServer_HostAddress}}/Stovariste/PronadjiMaterijal/1/Cement
// Accept: application/json

export async function pronadjiMaterijal(stovaristeId, search) {
  try {
    const response = await fetch(
      `${baseUrl}/PronadjiMaterijal/${stovaristeId}/${search}`
    );

    if (!response.ok) {
      const err = await response.text();
      return {
        error: err,
        data: null,
      };
    }

    const data = await response.json();

    const materijali = data.map((m) => {
      return new Materijal(
        m.materijal.id,
        m.materijal.naziv,
        m.materijal.datum,
        m.cena,
        m.kolicina
      );
    });

    return {
      error: null,
      data: materijali,
    };
  } catch (error) {
    console.error(error);
    return [];
  }
}

// PUT {{StovaristeServer_HostAddress}}/Stovariste/KupovinaMaterijala/1/2/310
// Accept: application/json

// [HttpPut("KupovinaMaterijala/{stovaristeID}/{materijalID}/{kolicina}")]

export async function kupiMaterijal(stovaristeId, materijalId, kolicina) {
  try {
    const response = await fetch(
      `${baseUrl}/KupovinaMaterijala/${stovaristeId}/${materijalId}/${kolicina}`,
      {
        method: "PUT",
      }
    );
    const data = response.text();

    return data;
  } catch (error) {
    console.error(error);
    return "Greska pri kupovini";
  }
}

// "id": 3,
// "naziv": "Pesak",
// "datum": "2025-01-26T13:49:28.7863087+01:00",
// "magaciniSaMaterijalom": null

// GET {{StovaristeServer_HostAddress}}/Stovariste/MaterijalUNajvecojKolicini/1
// Accept: application/json

export async function materijalUNajvecojKolicini(stovaristeId) {
  try {
    const response = await fetch(
      `${baseUrl}/MaterijalUNajvecojKolicini/${stovaristeId}`
    );

    if (!response.ok) {
      const err = await response.text();
      return {
        error: err,
        data: null,
      };
    }

    const data = await response.json();

    return {
      error: null,
      data: {
        id: data.id,
        naziv: data.naziv,
        datum: data.datum,
      },
    };
  } catch (error) {
    console.error(error);
    return {
      error: "Greska prilikom pribavljanja materijala sa najvecom kolicinom",
      data: null,
    };
  }
}
