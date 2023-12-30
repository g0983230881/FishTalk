const mongoose = require('mongoose');

var dbName = "FishDB";
var collectionName = "FishCollection";
var first_id = '6438cc71d6e3e690fbc8fc9c';

mongoose.set('strictQuery', true);

mongoose.connect('mongodb://127.0.0.1:27017/'+dbName, { useNewUrlParser: true })
  .then(() => {
    console.log('Connected to '+dbName);
  })
  .catch((error) => {
    console.error('Error connecting to MongoDB', error);
  });

const fishSchema = new mongoose.Schema({
    rctime: String,
    Chlorine: Number,
    Salinity: Number,
    PH: Number,
    DO: Number,
    EC: Number,
    Turbidity: Number,
    Chlorophyll: Number,
    ORP: Number,
    NH4: Number,
    Temperature: Number,
    WL: Number,
    date: String,
    pool: String,
    isDead: Number,
}, { collection: collectionName });

const fishModel = mongoose.model('fishModel', fishSchema);

fishModel.findOne({ _id: { $gt: first_id }}, function(err, nextDocument) {
  if (err) {
    console.log(err);
  } else {
    console.log(nextDocument);
  }
});
