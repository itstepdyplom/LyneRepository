"use client";

import React, { useState } from "react";
import {
  Drawer,
  List,
  ListItem,
  ListItemText,
  ListItemButton,
  Box,
  IconButton,
  Typography,
  Button,
  Divider,
} from "@mui/material";
import {
  Close as CloseIcon,
  ChevronRight as ChevronRightIcon,
} from "@mui/icons-material";
import { useAuthStore } from "../../stores/authStore";
import Link from "next/link";
import { useTranslations } from "next-intl";
import { useParams } from "next/navigation";
import { listItemTextSx, menuButtonSx } from "./Header.styles";
import { categories } from "../../constants/menu";

interface MobileMenuProps {
  open: boolean;
  onClose: () => void;
}

const MobileMenu: React.FC<MobileMenuProps> = ({ open, onClose }) => {
  const { isAuthenticated, user, logout } = useAuthStore();
  const t = useTranslations("Header");
  const params = useParams();
  const locale = params.locale as string;
  const [selectedSegment, setSelectedSegment] = useState<'mid' | 'premium'>('mid');

  const handleSegmentChange = (segment: 'mid' | 'premium') => {
    console.log('Changing segment to:', segment);
    setSelectedSegment(segment);
  };

  console.log('Current selectedSegment:', selectedSegment);

  return (
    <Drawer
      anchor="left"
      open={open}
      onClose={onClose}
      sx={{
        "& .MuiDrawer-paper": {
          width: { xs: "100%", sm: 360, md: 477 },
          background: selectedSegment === 'premium' 
            ? "linear-gradient(135deg, #FFE5E0 0%, #F0E8FF 50%, #E3F2FD 100%)" 
            : "#FFFFFF",
          borderRadius: { xs: 0, sm: "15px" },
          padding: { xs: "24px 20px 16px 24px", sm: "24px 20px 16px 24px", md: "24px 20px 16px 24px" },
          height: "100%",
          maxHeight: { md: 780 },
          transition: "background 0.3s ease",
          marginTop: { xs: "16px", sm: "20px", md: "24px" },
          marginLeft: { xs: "8px", sm: "12px", md: "16px" },
        },
      }}
    >
      {/* Header */}
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
          marginTop: { xs: "8px", sm: "12px", md: "16px" },
        }}
      >
        <IconButton onClick={onClose} sx={{ color: "black" }}>
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
          onClick={() => handleSegmentChange('mid')}
          sx={{
            ...menuButtonSx,
            backgroundColor: selectedSegment === 'mid' ? "#FFFFFF" : "#969696",
            color: selectedSegment === 'premium' ? "#FFFFFF" : "inherit",
            fontSize: { xs: "14px", sm: "15px", md: "17px" },
            whiteSpace: "nowrap",
            overflow: "hidden",
            textOverflow: "ellipsis",
            transition: "all 0.3s ease",
            transform: selectedSegment === 'mid' ? "scale(1.02)" : "scale(1)",
            boxShadow: selectedSegment === 'mid' ? "0 2px 8px rgba(0,0,0,0.1)" : "none",
          }}
        >
          Mid-Segment
        </Button>
        <Button
          onClick={() => handleSegmentChange('premium')}
          sx={{
            ...menuButtonSx,
            backgroundColor: selectedSegment === 'premium' ? "#FFFFFF" : "#969696",
            fontSize: { xs: "15px", sm: "17px", md: "19px" },
            background: selectedSegment === 'premium' 
              ? "#FFFFFF"
              : "linear-gradient(135deg, #FFBFB0, #D7D3F3, #86B7FF)",
            WebkitBackgroundClip: selectedSegment === 'premium' ? "initial" : "text",
            WebkitTextFillColor: selectedSegment === 'premium' ? "transparent" : "transparent",
            color: selectedSegment === 'premium' ? "transparent" : "inherit",
            whiteSpace: "nowrap",
            overflow: "hidden",
            textOverflow: "ellipsis",
            transition: "all 0.3s ease",
            transform: selectedSegment === 'premium' ? "scale(1.02)" : "scale(1)",
            boxShadow: selectedSegment === 'premium' ? "0 2px 8px rgba(0,0,0,0.1)" : "none",
            position: "relative",
            "&::after": selectedSegment === 'premium' ? {
              content: '"Premium"',
              position: "absolute",
              top: "50%",
              left: "50%",
              transform: "translate(-50%, -50%)",
              background: "linear-gradient(135deg, #FFBFB0, #D7D3F3, #86B7FF)",
              WebkitBackgroundClip: "text",
              WebkitTextFillColor: "transparent",
              fontSize: "inherit",
              fontWeight: "inherit",
              pointerEvents: "none",
            } : {},
          }}
        >
          {selectedSegment === 'premium' ? '' : 'Premium'}
        </Button>
      </Box>
    </Drawer>
  );
};

export default MobileMenu;