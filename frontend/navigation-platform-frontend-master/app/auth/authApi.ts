import axios from "axios";

// Axios instance with credentials enabled
export const api = axios.create({
  baseURL: "/api",
  withCredentials: true, // this sends cookies to the backend
});

export const login = async () => {
  // Redirect to OIDC login (PKCE logic is handled server-side)
  window.location.href = "/api/auth/login";
};

export const logout = async () => {
  await api.post("/auth/logout");
};

export const refreshToken = async () => {
  return api.get("/auth/refresh");
};

export const getUserProfile = async () => {
  return api.get("/auth/me"); // Returns user info if authenticated
};
