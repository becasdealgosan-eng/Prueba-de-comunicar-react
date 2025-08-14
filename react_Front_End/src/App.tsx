import React, { FC, useState, useEffect } from "react";

const App: FC = () => {
  const [contador, setContador] = useState<number>(0);

  useEffect(() => {
    const intervalo = setInterval(() => {
      fetch("http://localhost:5234/api/robot/posicion")
        .then((res) => res.json())
        .then((data) => setContador(Number(data)))
        .catch((err) => console.error("Error al obtener contador:", err));
    }, 1000); // cada 1 segundo

    return () => clearInterval(intervalo);
  }, []);

  return (
    <div style={{ padding: "16px", fontFamily: "sans-serif" }}>
      <h1>Contador en vivo</h1>
      <div style={{ fontSize: "2rem", fontWeight: "bold" }}>{contador}</div>
    </div>
  );
};

export default App;
