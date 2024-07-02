const assetFieldMap = new Map([
  ["id", "ID"],
  ["createdDate", "Created"],
  ["modifiedDate", "Modified"],
  ["assetId", "Asset ID"],
  ["tagNumber", "Tag No."],
  ["rateUnit", "Unit"],
  ["firstName", "First Name"],
  ["secondName", "Second Name"],
  ["phoneNum", "Phone"],
]);

const reverseAssetFieldMap = new Map();
assetFieldMap.forEach((value, key) => {
  reverseAssetFieldMap.set(value, key);
});

const bookingFieldMap = new Map([
  ["id", "ID"],
  ["createdDate", "Created"],
  ["modifiedDate", "Modified"],
  ["startDate", "Start"],
  ["endDate", "End"],
  ["productionID", "Prod ID"],
  ["productionName", "Production"],
  ["locationId", "Location ID"],
  ["locationName", "Location"],
  ["isProvisional", "Confirmed"],
  ["isRemote", "Remote"],
]);

const reverseBookingFieldMap = new Map();
bookingFieldMap.forEach((value, key) => {
  reverseBookingFieldMap.set(value, key);
});

function capitaliseFirstLetter(str) {
  if (str.length === 0) {
    return str;
  }
  return str.charAt(0).toUpperCase() + str.slice(1);
}

function decapitaliseFirstLetter(str) {
  if (str.length === 0) {
    return str;
  }
  return str.charAt(0).toLowerCase() + str.slice(1);
}

export function getAssetFriendlyName(fieldName) {
  if (assetFieldMap.has(fieldName)) {
    return assetFieldMap.get(fieldName);
  } else {
    return capitaliseFirstLetter(fieldName);
  }
}

export function getAssetVariableName(friendlyName) {
  // Check if the friendly name exists in the reverse mapping
  if (reverseAssetFieldMap.has(friendlyName)) {
    return reverseAssetFieldMap.get(friendlyName);
  } else {
    // If the friendly name is not found in the reverse mapping, return the original friendly name
    return decapitaliseFirstLetter(friendlyName);
  }
}
export function getBookingFriendlyName(fieldName) {
  if (bookingFieldMap.has(fieldName)) {
    return bookingFieldMap.get(fieldName);
  } else {
    return capitaliseFirstLetter(fieldName);
  }
}

export function getBookingVariableName(friendlyName) {
  // Check if the friendly name exists in the reverse mapping
  if (reverseBookingFieldMap.has(friendlyName)) {
    return reverseBookingFieldMap.get(friendlyName);
  } else {
    // If the friendly name is not found in the reverse mapping, return the original friendly name
    return decapitaliseFirstLetter(friendlyName);
  }
}
