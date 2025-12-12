"use client"
import React from 'react';
import { Box, Typography, IconButton,
  Pagination,
  Button, Grid } from '@mui/material';
import { useTranslations } from 'next-intl';
import FavoriteBorderIcon from "@mui/icons-material/FavoriteBorder";
import ViewModuleIcon from "@mui/icons-material/ViewModule";
import ViewListIcon from "@mui/icons-material/ViewList";
import ShoppingBagOutlinedIcon from '@mui/icons-material/ShoppingBagOutlined';
import TuneOutlinedIcon from '@mui/icons-material/TuneOutlined';
import { useState } from "react";
const accessoriesItems = [
  {
    id: 1,
    name: "Leather Belt",
    price: "1200 UAH",
    image: "https://turtle.com.ua/image/cache/catalog/%20%D1%80%D0%B5%D0%BC%D0%BD%D0%B8/classik/40kl/black/1shkirjanijremin40chornij-750x1000.jpg",
  },
  {
    id: 2,
    name: "Classic Sunglasses",
    price: "1800 UAH",
    image: "https://twosvge.com/cdn/shop/products/classic-black-female-sunglasses_800x.jpg?v=1710812315",
  },
  {
    id: 3,
    name: "Silk Scarf",
    price: "950 UAH",
    image: "https://uk.silksilky.com/cdn/shop/files/1603004566_7c8e1b0d-c9e9-482a-9497-54e0a16fbd8c.jpg?v=1762889111",
  },
  {
    id: 4,
    name: "Gold Watch",
    price: "8500 UAH",
    image: "https://au.danielwellington.com/cdn/shop/products/582aa857de34b6c7682b2a424b759e322e1be34a.png?v=1761566342",
  },
  {
    id: 5,
    name: "Leather Wallet",
    price: "2200 UAH",
    image: "https://hidemont.com.ua/media/catalog/product/cache/f21f6b40a0706b929db7661c49807c8a/1/_/1_0195_etn-configuration.jpg",
  },
  {
    id: 6,
    name: "Baseball Cap",
    price: "700 UAH",
    image: "https://gard.com.ua/image/cache/catalog/image/cache/catalog/shop/products/255f4e14-1cca-11ef-80d6-ba4fdc50ab5f-930x1240.webp",
  },
  {
    id: 7,
    name: "Fashion Ring",
    price: "450 UAH",
    image: "https://www.bhindi.com/upload/category/bhindi-fashion-rings-1728082149.jpg",
  },
  {
    id: 8,
    name: "Elegant Earrings",
    price: "650 UAH",
    image: "https://i.etsystatic.com/6120089/r/il/07a408/4181783930/il_570xN.4181783930_tcwu.jpg",
  },
  {
    id: 9,
    name: "Necklace",
    price: "1200 UAH",
    image: "https://carrieelizabeth.co.uk/cdn/shop/files/IMG_0412copy2_bc569849-d7da-4ac6-b546-839e6ec3a021.jpg?v=1706813421",
  },
  {
    id: 10,
    name: "Bracelet",
    price: "800 UAH",
    image: "https://www.bohomoon.com/cdn/shop/files/bohomoon-dainty-ball-bracelet-waterproof-tarnish-free-stainless-bracelets-36100411359409_1600x.jpg?v=1763017509",
  },
  {
    id: 11,
    name: "Beanie Hat",
    price: "550 UAH",
    image: "https://xcdn.next.co.uk/common/items/default/default/itemimages/3_4Ratio/product/lge/944970s.jpg?im=Resize,width=750",
  },
  {
    id: 12,
    name: "Leather Gloves",
    price: "1500 UAH",
    image: "https://iam-store.com/cdn/shop/files/ECA45379-93F2-4C88-B6C9-1AB56F2FCF77.png?v=1732950184",
  },
];

export default function AccessoriesPage() {
  const t = useTranslations('Categories');
const [view, setView] = useState<"four" | "two">("four");
  const [page, setPage] = useState(1);
  const itemsPerPage = 12;

  const handleChangePage = (_: any, value: number) => setPage(value);

  return (
    <Box sx={{ width: "100%", p: 3 }}>
      {/* Header */}
      <Box
        sx={{
          mb: 3,
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
        }}
      >
        <Typography sx={{ fontSize: 16, fontWeight:400 }}>
          <IconButton size="medium">
                <TuneOutlinedIcon fontSize="medium" />
            </IconButton>
            Filters & Sort
        </Typography>
        <Typography variant="h6" sx={{ fontWeight: 400, fontSize:24, mr:12 }}>
          Accessories
        </Typography>

        <Box sx={{ display: "flex", gap: 1 }}>
          <IconButton onClick={() => setView("four")}>
            <ViewModuleIcon />
          </IconButton>
          <IconButton onClick={() => setView("two")}>
            <ViewListIcon />
          </IconButton>
        </Box>
      </Box>

      {/* Grid */}
      <Grid
        container
        spacing={3}
        sx={{
          display: "grid",
          gridTemplateColumns:
            view === "four"
        ? { xs: "1fr", sm: "1fr 1fr", md: "repeat(4, 1fr)" } 
        : { xs: "1fr", sm: "1fr 1fr", md: "1fr 1fr" },
          gap: 3,
        }}
      >
        {accessoriesItems.slice(0, page * itemsPerPage).map((item) => (
          <Box
            key={item.id}
            sx={{
              borderRadius: 2,
              overflow: "hidden",
              position: "relative",
              background: "#fff",
              boxShadow: "0 2px 5px rgba(0,0,0,0.05)",
            }}
          >
            {/* Top Icons */}
            <Box
              sx={{
                position: "absolute",
                top: 10,
                right: 10,
                display: "flex",
                gap: 1,
              }}
            >
              <IconButton size="medium">
                <ShoppingBagOutlinedIcon fontSize="medium" />
              </IconButton>
              <IconButton size="medium">
                <FavoriteBorderIcon fontSize="medium" />
              </IconButton>
            </Box>

            <Box 
              component="img"
              src={item.image}
              alt={item.name} 
              sx={{
                width: "100%",
                height: view === "four" ? 380 : 500,
                objectFit: "cover",
                backgroundColor:"#F8F8F8"
              }} >
            </Box>
            {/* Title + Price */}
            <Box sx={{ p: 2, display:"flex", flexDirection:"column", alignItems:"center" }}>
              <Typography sx={{ fontSize: 16, color:"#2C2B2B" }}>{item.name}</Typography>
              <Typography sx={{ fontSize: 16, fontWeight: 400, color:"#7B8487"  }}>
                {item.price}
              </Typography>
            </Box>
          </Box>
        ))}
      </Grid>

      {/* Load more */}
      <Box sx={{ textAlign: "center", mt: 4 }}>
        <Button variant="text" sx={{color:"#817E7E"}}>Load more</Button>

        <Pagination
          count={5}
          page={page}
          onChange={handleChangePage}
          sx={{ mt: 2, display: "flex", justifyContent: "center", fontSize:16, fontWeight:300, color:"#817E7E" }}
        />
      </Box>
    </Box>
  );
} 