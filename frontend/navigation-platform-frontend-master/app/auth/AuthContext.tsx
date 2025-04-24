import React, { createContext, useEffect, useState } from "react";
import { getUserProfile, refreshToken, logout } from "./authApi";

type User = {
  id: string;
  email: string;
  name?: string;
  roles?: string[];
};

type AuthContextType = {
  user: User | null;
  loading: boolean;
  login: () => void;
  logout: () => void;
};

export const AuthContext = createContext<AuthContextType>({
  user: null,
  loading: true,
  login: () => {},
  logout: () => {},
});

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(true);

  const handleLogin = () => {
    window.location.href = "/api/auth/login";
  };

  const handleLogout = async () => {
    await logout();
    setUser(null);
  };

  // On first load, check session
  useEffect(() => {
    const fetchUser = async () => {
      try {
        const { data } = await getUserProfile();
        setUser(data);
      } catch (error) {
        // Optional: try to silently refresh
        try {
          await refreshToken();
          const { data } = await getUserProfile();
          setUser(data);
        } catch {
          setUser(null);
        }
      } finally {
        setLoading(false);
      }
    };

    fetchUser();
  }, []);

  return (
    <AuthContext.Provider
      value={{ user, loading, login: handleLogin, logout: handleLogout }}
    >
      {children}
    </AuthContext.Provider>
  );
};
