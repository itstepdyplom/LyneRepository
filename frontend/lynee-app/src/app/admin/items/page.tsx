"use client";

import {
  Box,
  Typography,
  IconButton,
  FormControl,
  Select,
  MenuItem,
  Button,
} from "@mui/material";
import Image from "next/image";
import AddIcon from "@mui/icons-material/Add";
import {
  DeleteForeverOutlined,
  BorderColorOutlined,
} from "@mui/icons-material";
import { useState } from "react";

const items = [
  {
    img: "/img/look/3.png",
    name: "Elegant Brown Suit",
    brand: "Jimmy Choo",
    code: "236574",
    price: "2500$",
    quantity: "3 item",
    status: "sold out",
  },
  {
    img: "/img/look/1.png",
    name: "Blue skinny dress",
    brand: "Ralph Lauren",
    code: "456456",
    price: "500$",
    quantity: "1 item",
    status: "available",
  },
  {
    img: "/img/look/2.png",
    name: "Polo Dress Kids",
    brand: "Ralph Lauren",
    code: "346372",
    price: "350$",
    quantity: "4 item",
    status: "available",
  },
];

export default function ItemsPage() {
  const [value1, setValue1] = useState("Recently Added");
  const [value2, setValue2] = useState("Premium");
  const [value3, setValue3] = useState("Dresses");

  return (
    <Box
      sx={{
        width: "100%",
        backgroundColor: "#fff",
        height: "100vh",
        overflowY: "auto",
        p: { xs: 1, md: 4 },
      }}
    >
      <Box
        sx={{
          display: "flex",
          flexDirection: { xs: "column", sm: "row" },
          gap: { xs: 2, sm: 2 },
          flex: 1,
          justifyContent:"flex-end"
        }}
      >
        <Button
          href={`/admin/items/new`}
          fullWidth
          variant="contained"
          sx={{
            background: "#1A1D23",
            color: "white",
            height: 48,
            textTransform: "none",
            fontWeight: 500,
            fontSize: { xs: 14, sm: 16 },
            mt: 2,
            width: "209px",
          }}
        >
          Add new item
          <AddIcon/>
        </Button>
      </Box>

      {/* FILTERS */}
      <Box
        sx={{
          display: "flex",
          flexDirection: { xs: "column", sm: "row" },
          gap: { xs: 2, sm: 5 },
          mb: 3,
        }}
      >
        {[value1, value2, value3].map((val, idx) => {
          const setters = [setValue1, setValue2, setValue3];
          const options = [
            ["Recently Added"],
            ["Premium", "Base"],
            ["Dresses", "Jewelery"],
          ];

          return (
            <FormControl
              key={idx}
              size="small"
              sx={{
                minWidth: 150,
                borderBottom: "2px solid gray",
                "& .MuiOutlinedInput-root": {
                  p: 0,
                  "& fieldset": { border: "none" },
                  "&:hover fieldset": { border: "none" },
                  "&.Mui-focused fieldset": { border: "none" },
                  borderRadius: 0,
                },
              }}
            >
              <Select
                value={val}
                onChange={(e) => setters[idx](e.target.value)}
                displayEmpty
                sx={{ fontSize: { xs: 14, sm: 16, md: 18 }, fontWeight: "500" }}
              >
                {options[idx].map((o, i) => (
                  <MenuItem
                    key={i}
                    value={o}
                    sx={{ fontSize: { xs: 14, sm: 16, md: 18 } }}
                  >
                    {o}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          );
        })}
      </Box>

      {/* TABLE */}
      <Box sx={{ overflowX: "auto" }}>
        {/* HEADER ROW */}
        <Box
          sx={{
            display: "grid",
            gridTemplateColumns: {
              xs: "80px 1fr 1fr 1fr 1fr 1fr 1fr 100px",
              md: "100px 1fr 1fr 1fr 1fr 1fr 1fr 100px",
            },
            py: 1.5,
            background: "#fff",
            borderBottom: "1px solid #e6e6e6",
            fontWeight: 600,
            fontSize: { xs: 12, sm: 14, md: 14 },
            color: "black",
          }}
        >
          <Box />
          <Typography>Name of the item</Typography>
          <Typography>Brand</Typography>
          <Typography>Code</Typography>
          <Typography>Price</Typography>
          <Typography>Quantity</Typography>
          <Typography>Status</Typography>
          <Box />
        </Box>

        {/* ITEMS */}
        {items.map((item, i) => (
          <Box
            key={i}
            sx={{
              display: "grid",
              gridTemplateColumns: {
                xs: "60px 1fr 1fr 1fr 1fr 1fr 1fr 80px",
                md: "80px 1fr 1fr 1fr 1fr 1fr 1fr 100px",
              },
              px: { xs: 1, md: 2 },
              py: 2,
              borderBottom: "1px solid #E5E7EB",
              alignItems: "center",
              "&:hover": { backgroundColor: "#fafafa" },
            }}
          >
            {/* IMAGE */}
            <Box
              sx={{
                width: { xs: 60, md: 80 },
                height: { xs: 60, md: 80 },
                position: "relative",
                backgroundImage: "url(/img/background.png)",
                backgroundSize: "cover",
                backgroundRepeat: "no-repeat",
              }}
            >
              <Image
                src={item.img}
                alt={item.name}
                fill
                style={{ objectFit: "cover", borderRadius: 6, }}
              />
            </Box>

            {/* NAME */}
            <Typography
              sx={{ fontSize: { xs: 12, sm: 14, md: 16 }, color: "black", ml:2 }}
            >
              {item.name}
            </Typography>

            <Typography
              sx={{ fontSize: { xs: 12, sm: 14, md: 16 }, color: "black" }}
            >
              {item.brand}
            </Typography>
            <Typography
              sx={{ fontSize: { xs: 12, sm: 14, md: 16 }, color: "black" }}
            >
              {item.code}
            </Typography>
            <Typography
              sx={{ fontSize: { xs: 12, sm: 14, md: 16 }, color: "black" }}
            >
              {item.price}
            </Typography>
            <Typography
              sx={{ fontSize: { xs: 12, sm: 14, md: 16 }, color: "black" }}
            >
              {item.quantity}
            </Typography>

            {/* STATUS */}
            <Typography
              sx={{
                fontSize: { xs: 12, sm: 14, md: 15 },
                fontWeight: 700,
                color: item.status === "sold out" ? "#000" : "#333",
              }}
            >
              {item.status}
            </Typography>

            {/* ICONS */}
            <Box sx={{ display: "flex", gap: 1, alignItems: "center" }}>
              <IconButton size="small" href="/admin/items/0">
                <BorderColorOutlined sx={{ fontSize: { xs: 20, sm: 22, md: 24 }, color:"black" }} />
              </IconButton>
              <IconButton size="small">
                <DeleteForeverOutlined
                  sx={{ fontSize: { xs: 20, sm: 22, md: 24 }, color:"black" }}
                />
              </IconButton>
            </Box>
          </Box>
        ))}
      </Box>
    </Box>
  );
}
