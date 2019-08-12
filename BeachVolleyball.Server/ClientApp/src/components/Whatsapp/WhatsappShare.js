import React from "react";
import NewTabLink from "../NewTabLink";

const Share = ({ number, text, children, handleClick }) => {
  var href = "https://wa.me/";
  if (number) {
    href += number;
  }

  if (text) {
    href += "?text=" + encodeURI(text);
  }
  return (
    <NewTabLink href={href} onClick={handleClick}>
      {children}
    </NewTabLink>
  );
};

export default Share;
