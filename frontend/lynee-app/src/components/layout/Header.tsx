"use client";

import React, { useState } from "react";
import {
  AppBar,
  Toolbar,
  Typography,
  Box,
  IconButton,
  Badge,
  Drawer,
  List,
  ListItem,
  ListItemText,
  ListItemButton,
  useMediaQuery,
  useTheme,
  InputBase,
  alpha,
  Button,
  Divider,
  colors,
} from "@mui/material";
import {
  Menu as MenuIcon,
  ShoppingBag,
  Search as SearchIcon,
  Person as PersonIcon,
  Close as CloseIcon,
  ChevronRight as ChevronRightIcon,
  Height,
} from "@mui/icons-material";
import { useAuthStore } from "../../stores/authStore";
import { useCartStore } from "../../stores/cartStore";
import Link from "next/link";
import { useTranslations } from "next-intl";
import { useParams } from "next/navigation";
import { listItemTextSx, menuButtonSx } from "./Header.styles";
import { categories } from "../../constants/menu";

const Header: React.FC = () => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("md"));
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
  const [searchOpen, setSearchOpen] = useState(false);

  const { isAuthenticated, user, logout } = useAuthStore();
  const { totalItems, openCart } = useCartStore();
  const t = useTranslations("Header");
  const params = useParams();
  const locale = params.locale as string;

  const handleDrawerToggle = () => {
    setMobileMenuOpen(!mobileMenuOpen);
  };

  const handleSearchToggle = () => {
    setSearchOpen(!searchOpen);
  };

  const mobileMenu = (
    <Drawer
      anchor="left"
      open={mobileMenuOpen}
      onClose={handleDrawerToggle}
      sx={{
        "& .MuiDrawer-paper": {
          width: { xs: "100%", sm: 360, md: 477 },
          backgroundColor: "#FFFFFF",
          borderRadius: { xs: 0, sm: "15px" },
          padding: 2,
          height: "100%",
          maxHeight: { md: 780 },
        },
      }}
    >
      {/* Header */}
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
        }}
      >
        <IconButton onClick={handleDrawerToggle} sx={{ color: "black" }}>
          <CloseIcon />
        </IconButton>
        <Typography
          sx={{
            ...listItemTextSx,
            color: "#000000",
            width: "59px",
            ml: 1,
            fontSize: { xs: "14px", sm: "16px", md: "18px" },
          }}
        >
          {t("close")}
        </Typography>
      </Box>

      {/* Categories */}
      <Box>
        <List>
          {categories.map((category) => (
            <ListItem key={category.key} disablePadding>
              <ListItemButton
                component={Link}
                href={category.href}
                sx={{ px: 0 }}
              >
                <ListItemText
                  primary={t(category.key)}
                  primaryTypographyProps={{ sx: listItemTextSx }}
                />
                {category.hasSubmenu && <ChevronRightIcon fontSize="medium" />}
              </ListItemButton>
            </ListItem>
          ))}
        </List>
      </Box>

      <Divider />

      {/* Account / Auth */}
      <Box>
        {isAuthenticated ? (
          <>
            <ListItem>
              <ListItemText
                primary={t("welcome", { name: user?.name || "User" })}
                primaryTypographyProps={{ variant: "body2" }}
              />
            </ListItem>
            <ListItem disablePadding>
              <ListItemButton onClick={logout}>
                <ListItemText
                  primary={t("logout")}
                  primaryTypographyProps={{ variant: "subtitle2" }}
                />
              </ListItemButton>
            </ListItem>
          </>
        ) : (
          <>
            <ListItem disablePadding>
              <ListItemButton
                component={Link}
                href={`/${locale}/auth/login`}
                sx={{ px: 0, mt: 1, height: { xs: 28, md: 30 } }}
              >
                <ListItemText
                  primary={t("login")}
                  primaryTypographyProps={{
                    sx: {
                      ...listItemTextSx,
                      fontSize: { xs: "16px", md: "19px" },
                    },
                  }}
                />
              </ListItemButton>
            </ListItem>
            {/*<ListItem disablePadding>
                <ListItemButton component={Link} href={`/${locale}/auth/register`}>
                  <ListItemText
                    primary={t('register')}
                    primaryTypographyProps={{ variant: 'subtitle2' }}
                  />
                </ListItemButton>
              </ListItem>*/}
            <ListItem disablePadding>
              <ListItemButton
                component={Link}
                href={``}
                sx={{ px: 0, mt: 1, mb: 1, height: { xs: 28, md: 30 } }}
              >
                <ListItemText
                  primary={t("wishlist")}
                  primaryTypographyProps={{
                    sx: {
                      ...listItemTextSx,
                      fontSize: { xs: "16px", md: "19px" },
                    },
                  }}
                />
              </ListItemButton>
            </ListItem>
          </>
        )}
      </Box>

      <Divider />

      {/* Extra */}
      <List>
        <ListItemButton sx={{ px: 0, mt: 1, height: { xs: 28, md: 30 } }}>
          <ListItemText
            primary={t("contact")}
            primaryTypographyProps={{
              sx: { ...listItemTextSx, fontSize: { xs: "16px", md: "19px" } },
            }}
          />
        </ListItemButton>
        <ListItemButton sx={{ px: 0, mt: 1, height: { xs: 28, md: 30 } }}>
          <ListItemText
            primary={t("changeLocationAndLanguage")}
            primaryTypographyProps={{
              sx: { ...listItemTextSx, fontSize: { xs: "16px", md: "19px" } },
            }}
          />
        </ListItemButton>
      </List>

      {/* Bottom Buttons */}
      <Box
        sx={{
          position: "absolute",
          bottom: 16,
          left: 16,
          right: 16,
          display: "flex",
          borderRadius: "5px",
          border: "1px solid #969696",
          height: { xs: 30, md: 34 },
          backgroundColor: "#969696",
        }}
      >
        <Button
          sx={{
            ...menuButtonSx,
            backgroundColor: "#FFFFFF",
            fontSize: { xs: "14px", sm: "15px", md: "17px" },
          }}
        >
          Mid - Segment
        </Button>
        <Button
          sx={{
            ...menuButtonSx,
            backgroundColor: "#969696",
            fontSize: { xs: "15px", sm: "17px", md: "19px" },
            background: "linear-gradient(90deg, #FFBFB0, #D7D3F3, #86B7FF )",
            WebkitBackgroundClip: "text",
            WebkitTextFillColor: "transparent",
          }}
        >
          Premium
        </Button>
      </Box>
    </Drawer>
  );

  return (
    <>
      <AppBar
        position="sticky"
        elevation={0}
        sx={{ backgroundColor: "white", color: "black" }}
      >
        <Toolbar sx={{ justifyContent: "space-between", px: { xs: 2, md: 4 } }}>
          <Box sx={{ display: "flex", alignItems: "center" }}>
            <IconButton
              edge="start"
              onClick={handleDrawerToggle}
              sx={{ mr: 2 }}
            >
              <MenuIcon />
            </IconButton>
          </Box>

          {/* Center - Logo */}
          <Box
            sx={{
              position: "absolute",
              left: "50%",
              transform: "translateX(-50%)",
            }}
          >
            <Typography
              variant="h4"
              component={Link}
              href={`/${locale}`}
              sx={{
                fontWeight: 300,
                letterSpacing: "0.2em",
                textDecoration: "none",
                color: "inherit",
                fontSize: { xs: "1.5rem", md: "2rem" },
              }}
            >
              LYNE
            </Typography>
          </Box>

          <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
            {searchOpen ? (
              <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
                <InputBase
                  placeholder={t("search")}
                  autoFocus
                  sx={{
                    fontSize: "1rem",
                    backgroundColor: "white",
                    borderRadius: 1,
                    px: 2,
                    py: 1,
                    border: "1px solid",
                    borderColor: "divider",
                    minWidth: 200,
                    "& input": {
                      py: 0.5,
                    },
                  }}
                />
                <IconButton onClick={handleSearchToggle} size="small">
                  <CloseIcon />
                </IconButton>
              </Box>
            ) : (
              <IconButton onClick={handleSearchToggle}>
                <SearchIcon />
              </IconButton>
            )}

            <IconButton component={Link} href={`/${locale}/account`}>
              <PersonIcon />
            </IconButton>

            <IconButton component={Link} href={`/${locale}/cart`}>
              <Badge badgeContent={totalItems} color="primary">
                <ShoppingBag />
              </Badge>
            </IconButton>
          </Box>
        </Toolbar>
      </AppBar>

      {mobileMenu}
    </>
  );
};

export default Header;
