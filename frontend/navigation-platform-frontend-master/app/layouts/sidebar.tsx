// components/SidebarLayout.tsx
import { Sidebar } from "primereact/sidebar";
import { Button } from "primereact/button";
import "primeflex/primeflex.css";
import { Link, Outlet, useLocation } from "react-router";
import { useEffect, useState } from "react";
import { ConfirmDialog, confirmDialog } from "primereact/confirmdialog";

const SIDEBAR_WIDTH = 250;

export default function SidebarLayout() {
  const location = useLocation();
  const [visible, setVisible] = useState(true);
  const [isMobile, setIsMobile] = useState(false);

  const sidebarBgColor = "#ffffff";
  const sidebarBorderColor = "#e0e0e0";
  const sidebarTextColor = "#333333";
  const hoverBgColor = "#f5f5f5";
  const activeBgColor = "#e8e8e8";
  const mainContentBgColor = "#fafafa";

  useEffect(() => {
    const handleResize = () => {
      const isSmall = window.innerWidth <= 768;
      setIsMobile(isSmall);
      setVisible(!isSmall); // hide sidebar on small screens initially
    };

    handleResize();
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  const navLinkStyle = (path: string) => ({
    padding: "10px 14px",
    color: sidebarTextColor,
    textDecoration: "none",
    borderRadius: "6px",
    backgroundColor: location.pathname === path ? activeBgColor : "transparent",
    transition: "background-color 0.3s",
  });

  const handleLogout = () => {
    confirmDialog({
      message: "Are you sure you want to logout?",
      header: "Confirm Logout",
      icon: "pi pi-sign-out",
      acceptLabel: "Yes",
      rejectLabel: "Cancel",
      accept: () => {
        // 👉 Replace this with actual logout logic
        console.log("Logging out...");
        window.location.href = "/login"; // or navigate using router
      },
    });
  };

  return (
    <div className="flex">
      {isMobile && (
        <Button
          icon="pi pi-bars"
          className="p-button-text"
          style={{
            position: "fixed",
            top: "10px",
            left: "10px",
            zIndex: 1000,
          }}
          onClick={() => setVisible(true)}
        />
      )}

      {/* Sidebar */}
      <Sidebar
        visible={visible}
        onHide={() => setVisible(false)}
        showCloseIcon={isMobile}
        modal={isMobile}
        dismissable={isMobile}
        style={{
          width: `${SIDEBAR_WIDTH}px`,
          height: "100vh",
          position: "fixed",
          top: 0,
          left: 0,
          borderRadius: 0,
          zIndex: 999,
          backgroundColor: sidebarBgColor,
          borderRight: `1px solid ${sidebarBorderColor}`,
        }}
      >
        <div
          className="p-3"
          style={{
            height: "100%",
            color: sidebarTextColor,
            display: "flex",
            flexDirection: "column",
            justifyContent: "space-between",
          }}
        >
          <div>
            <h2
              className="mb-4"
              style={{ fontSize: "1.5rem", fontWeight: "600" }}
            >
              My App
            </h2>

            <nav className="flex flex-column gap-2">
              <Link
                to="/journeys"
                style={navLinkStyle("/journeys")}
                onClick={() => isMobile && setVisible(false)}
                onMouseEnter={(e) =>
                  (e.currentTarget.style.backgroundColor = hoverBgColor)
                }
                onMouseLeave={(e) =>
                  (e.currentTarget.style.backgroundColor =
                    location.pathname === "/journeys"
                      ? activeBgColor
                      : "transparent")
                }
              >
                Journey List
              </Link>
              <Link
                to="/journey-details"
                style={navLinkStyle("/journey-details")}
                onClick={() => isMobile && setVisible(false)}
                onMouseEnter={(e) =>
                  (e.currentTarget.style.backgroundColor = hoverBgColor)
                }
                onMouseLeave={(e) =>
                  (e.currentTarget.style.backgroundColor =
                    location.pathname === "/journey-details"
                      ? activeBgColor
                      : "transparent")
                }
              >
                Journey Details
              </Link>
              <Link
                to="/badge-reward"
                style={navLinkStyle("/badge-reward")}
                onClick={() => isMobile && setVisible(false)}
                onMouseEnter={(e) =>
                  (e.currentTarget.style.backgroundColor = hoverBgColor)
                }
                onMouseLeave={(e) =>
                  (e.currentTarget.style.backgroundColor =
                    location.pathname === "/badge-reward"
                      ? activeBgColor
                      : "transparent")
                }
              >
                Daily Reward Badge
              </Link>
            </nav>
          </div>

          {/* 👇 Logout Button */}
          <div className="mt-4">
            <Button
              label="Logout"
              icon="pi pi-sign-out"
              className="p-button-danger p-button-outlined w-full"
              onClick={handleLogout}
            />
          </div>
        </div>
      </Sidebar>

      {/* Confirm Dialog Component */}
      <ConfirmDialog />

      {/* Main Content */}
      <div
        className="p-4"
        style={{
          marginLeft: isMobile ? 0 : SIDEBAR_WIDTH,
          flexGrow: 1,
          background: mainContentBgColor,
          minHeight: "100vh",
          width: "100%",
        }}
      >
        <Outlet />
      </div>
    </div>
  );
}
