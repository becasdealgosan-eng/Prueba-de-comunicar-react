// src/CallApi.tsx
export async function CallApi(
  api: string,
  utilidad: "POST" | "SEND",
  info: any = null
) {
  try {
    let options: RequestInit = {
      method: utilidad === "POST" ? "POST" : "GET",
      headers: {}
    };

    if (utilidad === "POST") {
      options.headers = { "Content-Type": "application/json" };
      options.body = JSON.stringify(info);
    }

    const response = await fetch(api, options);

    if (!response.ok) {
      throw new Error(`Error en la petición: ${response.status}`);
    }

    return await response.json();
  } catch (error) {
    console.error("Error en CallApi:", error);
    throw error;
  }
}
