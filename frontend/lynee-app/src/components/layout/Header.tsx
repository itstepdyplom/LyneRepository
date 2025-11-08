"use client";

import React, { useState } from "react";
import {
  AppBar,
  Toolbar,
  Typography,
  Box,
  IconButton,
  Badge,
  useMediaQuery,
  useTheme,
  InputBase,
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
import { useCartStore } from "../../stores/cartStore";
import Link from "next/link";
import { useTranslations } from "next-intl";
import { useParams } from "next/navigation";
import MobileMenu from "./mobileMenu";

const Header: React.FC = () => {
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
  const [searchOpen, setSearchOpen] = useState(false);

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

      <MobileMenu open={mobileMenuOpen} onClose={handleDrawerToggle} />
    </>
  );
};

export default Header;
