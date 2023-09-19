import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import MenuIcon from "@mui/icons-material/Menu";
import Container from "@mui/material/Container";
import LoginIcon from "@mui/icons-material/Login";
import Button from "@mui/material/Button";
import VpnKeyIcon from "@mui/icons-material/VpnKey";
import MenuItem from "@mui/material/MenuItem";
import AdbIcon from "@mui/icons-material/Adb";
import ButtonGroup from "@mui/material/ButtonGroup";
import { Link } from "react-router-dom";

import * as AuthService from "../services/authService";
import IUser from "../types/user.type";
import { useEffect, useState } from "react";
import EventBus from "../common/EventBus";

const pages = ["Two-Digit System", "Training","games"];

const Navbar: React.FC = () => {
  // const [showModeratorBoard, setShowModeratorBoard] = useState<boolean>(false);
  // const [showAdminBoard, setShowAdminBoard] = useState<boolean>(false);
  const [currentUser, setCurrentUser] = useState<IUser | undefined>(undefined);

  useEffect(() => {
    const user = AuthService.getCurrentUser();

    if (user) {
      setCurrentUser(user);
      // setShowModeratorBoard(user.roles.includes("ROLE_MODERATOR"));
      // setShowAdminBoard(user.roles.includes("ROLE_ADMIN"));
    }

    EventBus.on("logout", logOut);

    return () => {
      EventBus.remove("logout", logOut);
    };
  }, []);

  const logOut = () => {
    AuthService.logout();
    // setShowModeratorBoard(false);
    // setShowAdminBoard(false);
    setCurrentUser(undefined);
  };

  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(
    null
  );

  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  return (
    <AppBar position="static" color="default">
      <Container maxWidth="false">
        <Toolbar disableGutters>
          <AdbIcon sx={{ display: { xs: "none", md: "flex" }, mr: 1 }} />
          <Typography
            variant="h6"
            noWrap
            component="a"
            href="/"
            sx={{
              mr: 2,
              display: { xs: "none", md: "flex" },
              fontFamily: "monospace",
              fontWeight: 700,
              letterSpacing: ".3rem",
              color: "inherit",
              textDecoration: "none",
            }}
          >
            Palace Memory
          </Typography>

          <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleOpenNavMenu}
              color="inherit"
            >
              <MenuIcon />
            </IconButton>
            <Menu
              id="menu-appbar"
              anchorEl={anchorElNav}
              anchorOrigin={{
                vertical: "bottom",
                horizontal: "left",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "left",
              }}
              open={Boolean(anchorElNav)}
              onClose={handleCloseNavMenu}
              sx={{
                display: { xs: "block", md: "none" },
              }}
            >
              {pages.map((page) => (
                <MenuItem key={page} onClick={handleCloseNavMenu}>
                  <Typography textAlign="center">{page}</Typography>
                </MenuItem>
              ))}
            </Menu>
          </Box>
          <AdbIcon sx={{ display: { xs: "flex", md: "none" }, mr: 1 }} />
          <Typography
            variant="h5"
            noWrap
            component="a"
            href="/"
            sx={{
              mr: 2,
              display: { xs: "flex", md: "none" },
              flexGrow: 1,
              fontFamily: "monospace",
              fontWeight: 700,
              letterSpacing: ".3rem",
              color: "inherit",
              textDecoration: "none",
            }}
          >
            Palace Memory
          </Typography>
          <Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" }, xs: 1 }}>
            {pages.map((page) => (
              <Button
                key={page}
                component={Link}
                to={page}
                onClick={handleCloseNavMenu}
                sx={{ my: 1, color: "inherit", display: "block" }}
              >
                {page}
              </Button>
            ))}
          </Box>
{!currentUser ? (
            <Box sx={{ justifyContent: "flex-end" }}>
              <ButtonGroup>
                <IconButton sx={{ p: 1 }}>
                  <Button
                    component={Link}
                    to="login"
                    variant="contained"
                    color="secondary"
                    endIcon={<LoginIcon />}
                  >
                    Login
                  </Button>
                </IconButton>
                <IconButton sx={{ p: 1 }}>
                  <Button
                    component={Link}
                    to="register"
                    variant="outlined"
                    color="secondary"
                    endIcon={<VpnKeyIcon />}
                  >
                    Register
                  </Button>
                </IconButton>
              </ButtonGroup>
            </Box>
) : (
            <Box sx={{ justifyContent: "flex-end" }}>
              <ButtonGroup>
                <IconButton sx={{ p: 1 }}>
                  <Button
                    component={Link}
                    to="login"
                    onClick={logOut}
                    variant="contained"
                    color="secondary"
                    endIcon={<LoginIcon />}
                  >
                    Logout
                  </Button>
                </IconButton>
              </ButtonGroup>
            </Box>
)}
        </Toolbar>
      </Container>
    </AppBar>
  );
};
export default Navbar;
