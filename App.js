/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow strict-local
 */

import React from 'react';
import {
  SafeAreaView,
  StyleSheet,
  ScrollView,
  View,
  Text,
  StatusBar,
  requireNativeComponent,
  UIManager,
  findNodeHandle,
  Button,
  Image
} from 'react-native';

import {
  Header,
  LearnMoreLinks,
  Colors,
  DebugInstructions,
  ReloadInstructions,
} from 'react-native/Libraries/NewAppScreen';

import InkCanvas from './InkCanvasControl'

class App extends React.Component {

  constructor(props) {
    super(props);
    this.state = { inkImage: ''};
  }

  saveInk = () => {
    if (this._inkCanvasRef) {
      const tag = findNodeHandle(this._inkCanvasRef);
      UIManager.dispatchViewManagerCommand(tag, UIManager.getViewManagerConfig('InkCanvasControl').Commands.saveInkToFile, ['ink.jpg']);
    }
  }

  saveInkToBase64 = () => {
    if (this._inkCanvasRef) {
      const tag = findNodeHandle(this._inkCanvasRef);
      UIManager.dispatchViewManagerCommand(tag, UIManager.getViewManagerConfig('InkCanvasControl').Commands.saveInkToBase64);
    }
  }

  onGetInk = (result) => {
    console.log("Ink saved");
    var image = 'data:image/png;base64,' + result.nativeEvent;
    this.setState({ inkImage: image});
    console.log(result.nativeEvent);
  }

  render(){
    return (
      <View>
        <Button title="Save ink" onPress={() => {this.saveInk(); }} />
        <Button title="Save ink to base64" onPress={ () => {this.saveInkToBase64();}} />
        <InkCanvas ref={(ref) => { this._inkCanvasRef = ref;}}
                   style={{ width: 1000, height: 600}}
                   onInkSaved={(evt) => {this.onGetInk(evt); }} />

        <Image style={{width: 500, height: 400, resizeMode: 'cover'}} source={ {uri: this.state.inkImage}} />
      </View>
    );
  }
};

const styles = StyleSheet.create({
  scrollView: {
    backgroundColor: Colors.lighter,
  },
  engine: {
    position: 'absolute',
    right: 0,
  },
  body: {
    backgroundColor: Colors.white,
  },
  sectionContainer: {
    marginTop: 32,
    paddingHorizontal: 24,
  },
  sectionTitle: {
    fontSize: 24,
    fontWeight: '600',
    color: Colors.black,
  },
  sectionDescription: {
    marginTop: 8,
    fontSize: 18,
    fontWeight: '400',
    color: Colors.dark,
  },
  highlight: {
    fontWeight: '700',
  },
  footer: {
    color: Colors.dark,
    fontSize: 12,
    fontWeight: '600',
    padding: 4,
    paddingRight: 12,
    textAlign: 'right',
  },
});

export default App;
