import axios, { AxiosRequestConfig } from "axios";

const callApi = (method: string, url: string, variables?: any) =>
  new Promise((resolve, reject) => {
    axios({
      baseURL: import.meta.env.VITE_APP_API_URL,
      url: url,
      method,
      params: method === "GET" ? variables : undefined,
      data: method !== "GET" ? variables : undefined,
      headers: {
        "Content-Type": "application/json",
      },
      responseType: "json",
    } as AxiosRequestConfig).then(
      (response) => {
        resolve(response.data);
      },
      (error) => {
        if (error.response?.data) {
          reject(error.response.data.error || error.response.data.errors);
        } else {
          reject({
            code: "INTERNAL_ERROR",
            message: "An error occurred. Check your internet connection.",
            status: 503,
            data: {},
          });
        }
      }
    );
  });

export const api = {
  get: (url: string, variables?: any) => callApi("GET", url, variables),
  post: (url: string, variables?: any) => callApi("POST", url, variables),
  put: (url: string, variables?: any) => callApi("PUT", url, variables),
  patch: (url: string, variables?: any) => callApi("PATCH", url, variables),
  delete: (url: string, variables?: any) => callApi("DELETE", url, variables),
};
