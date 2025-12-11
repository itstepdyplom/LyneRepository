"use client";

import {
  Box,
  Typography,
  Button,
  TextField,
  Modal,
  Fade,
  Backdrop,
  IconButton,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import { useState } from "react";

interface SubcategoryModalProps {
  open: boolean;
  onClose: () => void;
  onSubmit: (data: { name: string; image: string }) => void;
}

export default function AddSubcategoryModal({
  open,
  onClose,
  onSubmit,
}: SubcategoryModalProps) {
  const [name, setName] = useState("");
  const [image, setImage] = useState("https://lsco.scene7.com/is/image/lsco/167860018-dynamic1-pdp?fmt=jpeg&qlt=70&resMode=sharp2&fit=crop,1&op_usm=0.6,0.6,8&wid=2000&hei=2500");

  const handleImageUpload = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;

    const reader = new FileReader();
    reader.onload = () => setImage(reader.result as string);
    reader.readAsDataURL(file);
  };

  const handleSubmit = () => {
    onSubmit({ name, image });
    setName("");
    setImage("");
    onClose();
  };

  return (
    <Modal
      open={open}
      onClose={onClose}
      closeAfterTransition
      BackdropComponent={Backdrop}
      BackdropProps={{
        timeout: 300,
        sx: { backgroundColor: "rgba(255,255,255,0.5)" },
      }}
    >
      <Fade in={open}>
        <Box
          sx={{
            position: "fixed",
            top: "50%",
            left: "80%",
            transform: "translate(-50%,-50%)",
            width: 510,
            height:600,
            bgcolor: "#2C2B2B",
            color: "#fff",
           // borderRadius: 2,
            p: 4,
            textAlign: "center",
          }}
        >
          {/* Close button */}
          <IconButton
            onClick={onClose}
            sx={{ position: "absolute", right: 12, top: 12, color: "white" }}
          >
            <CloseIcon />
          </IconButton>

         <Box sx={{width:"357px", ml:5}}>
          <Typography
            sx={{ textAlign: "left", mb: 1, fontSize: 16, fontWeight: 400, mt:5 }}
          >
            Name of the subcategory
          </Typography>

          <TextField
            fullWidth
            value={name}
            onChange={(e) => setName(e.target.value)}
            sx={{
              mb: 2,
              background: "#F6F6F6",
              borderRadius: 1,
              "& input": { padding: "10px" },
            }}
          />

          <Typography
            sx={{ textAlign: "left", mb: 1, fontSize: 16, fontWeight: 400 }}
          >
            Add photo of the category
          </Typography>

          {/* Preview or empty box */}
          <Box
            sx={{
              width: "100%",
              height: 210,
              background: "#2A2E35",
              borderRadius: 1,
              mb: 1.5,
              overflow: "hidden",
            }}
          >
            {image && (
              <Box
                component="img"
                src={image}
                sx={{
                  width: "100%",
                  height: "100%",
                  objectFit: "cover",
                }}
              />
            )}
          </Box>

          {/* Upload */}
          <Button
            component="label"
            sx={{
              background: "none",
              color: "#fff",
              mb: 3,
              textTransform: "none",
              "&:hover": { background: "transparent", opacity: 0.7 },
              fontSize: 14,
              fontWeight:400,
              display: "flex",
              gap: 1,
            }}
          >
            ⬆ Upload from your computer
            <input type="file" hidden onChange={handleImageUpload} />
          </Button>

          {/* Submit button */}
          <Button
            fullWidth
            onClick={handleSubmit}
            sx={{
              background: "#fff",
              color: "#2C2B2B",
              textTransform: "none",
              fontWeight: 400,
              height: 45,
              borderRadius: 1,
              fontSize: 20,
              "&:hover": { background: "#e5e5e5" },
            }}
          >
            Add the subcategory
          </Button>
          </Box>
        </Box>
      </Fade>
    </Modal>
  );
}
