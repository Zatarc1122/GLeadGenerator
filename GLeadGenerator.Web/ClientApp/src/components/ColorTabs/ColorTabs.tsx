import Tabs from "@mui/material/Tabs";
import Tab from "@mui/material/Tab";
import TabPanel from "@mui/lab/TabPanel";
import TabContext from "@mui/lab/TabContext";
import Box from "@mui/material/Box";

import { ContactPage, BusinessUsers } from "../../features";
import { SyntheticEvent, useState } from "react";

export function ColorTabs() {
  const [value, setValue] = useState("contact");

  const handleChange = (e: SyntheticEvent, newValue: string) => {
    setValue(newValue);
  };

  return (
    <>
      <TabContext value={value}>
        <Box sx={{ width: "100%" }}>
          <Tabs value={value} onChange={handleChange} aria-label="tabs">
            <Tab value="contact" label="Contact" />
            <Tab value="business" label="List of business users" />
          </Tabs>
        </Box>

        <TabPanel value="contact">
          <ContactPage />
        </TabPanel>
        <TabPanel value="business">
          <BusinessUsers />
        </TabPanel>
      </TabContext>
    </>
  );
}
