"use client";

import {
  Box,
  Typography,
  TextField,
  Button,
  MenuItem,
  Select,
  FormControl,
  IconButton,
} from "@mui/material";
import Image from "next/image";
import {
  PanoramaFishEyeOutlined,
  WestOutlined,
  BorderColorOutlined,
} from "@mui/icons-material";

const images = [
  "/img/items/1.png",
  "/img/items/2.png",
  "/img/items/3.png",
  "/img/items/4.png",
];

export default function ItemEditPage() {
  return (
    <Box sx={{ backgroundColor: "#FFFFFF", p: { xs: 1, sm: 2, md: 3 } }}>
      {/* BACK BUTTON */}
      <IconButton sx={{ mb: { xs: 1, sm: 2 }, mt: 3 }}>
        <PanoramaFishEyeOutlined fontSize="large" sx={{ color: "black" }} />
        <WestOutlined fontSize="large" sx={{ color: "black", ml: -3 }} />
      </IconButton>

      {/* MAIN LAYOUT */}
      <Box
        sx={{
          display: "flex",
          width: "100%",
          gap: 4,
          flexDirection: { xs: "column", lg: "row" },
        }}
      >
        {/* LEFT BLOCK — IMAGES */}
        <Box
          sx={{
            width: { xs: "100%", lg: "50%" },
            display: "grid",
            gridTemplateColumns: "1fr 1fr",
            gap: { xs: 1.5, sm: 2 },
          }}
        >
          {images.map((img, i) => (
            <Box
              key={i}
              sx={{
                width: "100%",
                height: { xs: 260, sm: 330, md: 380, lg: 450 },
                position: "relative",
                backgroundImage: "url(/img/background.png)",
                backgroundSize: "cover",
                backgroundRepeat: "no-repeat",
                borderRadius: 1,
                overflow: "hidden",
              }}
            >
              <Image src={img} alt="item" fill style={{ objectFit: "cover" }} />

              <Box
                sx={{
                  position: "absolute",
                  top: 10,
                  right: 10,
                  borderRadius: "50%",
                  p: 0.7,
                  cursor: "pointer",
                }}
              >
                <BorderColorOutlined sx={{ fontSize: 24, color: "white" }} />
              </Box>
            </Box>
          ))}
        </Box>

        {/* RIGHT BLOCK — FORM */}
        <Box
          sx={{
            width: { xs: "100%", lg: "50%" },
            display: "flex",
            flexDirection: "column",
            gap: 2,
          }}
        >
          {/* NAME + CODE */}
          <Box
            sx={{
              display: "flex",
              gap: 2,
              flexDirection: { xs: "column", md: "row" },
              color:"black"
            }}
          >
            <Box sx={{ display: "flex", flexDirection: "column", flex: 1 }}>
              <Typography sx={{ fontSize: 14, fontWeight: 300, mb: 0.5 }}>
                Name
              </Typography>
              <TextField fullWidth defaultValue="Elegant Brown Suit" />
            </Box>

            <Box sx={{ display: "flex", flexDirection: "column", width: { xs: "100%", md: 150 } }}>
              <Typography sx={{ fontSize: 14, fontWeight: 300, mb: 0.5 }}>
                Code
              </Typography>
              <TextField defaultValue="236574" />
            </Box>
          </Box>

          {/* SHORT DESC */}
          <Box>
            <Typography sx={{ fontSize: 14, fontWeight: 300, mb: 0.5, color:"black" }}>
              Short Description
            </Typography>
            <TextField
              fullWidth
              multiline
              rows={2}
              defaultValue="Sophisticated brown suit designed for a polished and confident look"
            />
          </Box>

          {/* BRAND */}
          <Box>
            <Typography sx={{ fontSize: 14, fontWeight: 300, mb: 0.5, color:"black" }}>
              Brand
            </Typography>
            <FormControl fullWidth>
              <Select defaultValue="Jimmy Choo">
                <MenuItem value="Jimmy Choo">Jimmy Choo</MenuItem>
                <MenuItem value="Prada">Prada</MenuItem>
                <MenuItem value="LV">Louis Vuitton</MenuItem>
              </Select>
            </FormControl>
          </Box>

          {/* QUANTITY */}
          <Box>
            <Typography sx={{ fontSize: 14, fontWeight: 300, mb: 0.5, color:"black" }}>
              Quantity
            </Typography>
            <FormControl fullWidth>
              <Select defaultValue="3 item">
                <MenuItem value="1 item">1 item</MenuItem>
                <MenuItem value="2 item">2 item</MenuItem>
                <MenuItem value="3 item">3 item</MenuItem>
              </Select>
            </FormControl>
          </Box>

          {/* COLOR */}
          <Box>
            <Typography sx={{ fontSize: 14, fontWeight: 300, mb: 0.5, color:"black" }}>
              Color
            </Typography>
            <FormControl fullWidth>
              <Select defaultValue="Brown">
                <MenuItem value="Brown">Brown</MenuItem>
                <MenuItem value="Black">Black</MenuItem>
                <MenuItem value="Beige">Beige</MenuItem>
              </Select>
            </FormControl>
          </Box>

          {/* SIZE */}
          <Box>
            <Typography sx={{ fontSize: 14, fontWeight: 300, mb: 0.5, color:"black" }}>
              Size
            </Typography>
            <FormControl fullWidth>
              <Select defaultValue="S-L">
                <MenuItem value="S-L">S-L</MenuItem>
                <MenuItem value="XS">XS</MenuItem>
                <MenuItem value="M">M</MenuItem>
              </Select>
            </FormControl>
          </Box>

          {/* PRICE */}
          <Box>
            <Typography sx={{ fontSize: 14, fontWeight: 300, mb: 0.5, color:"black" }}>
              Price
            </Typography>
            <TextField defaultValue="16400 UAH" fullWidth />
          </Box>

          {/* MATERIALS */}
          <Box>
            <Typography sx={{ fontSize: 14, fontWeight: 300, mb: 0.5, color:"black" }}>
              Fabric composition
            </Typography>
            <TextField
              multiline
              rows={5}
              defaultValue={`• Main: 70% Triacetate, 30% Polyester
• Lining: 100% Silk
• Lightweight, fluid fabric with a smooth finish and elegant drape.`}
              fullWidth
            />
          </Box>

          {/* BUTTON */}
          <Button
            sx={{
              backgroundColor: "#000",
              color: "#fff",
              height: 48,
              mt: 2,
              "&:hover": { backgroundColor: "#333" },
              fontWeight: 400,
              fontSize: 16,
            }}
          >
            Confirm changes
          </Button>
        </Box>
      </Box>
    </Box>
  );
}
