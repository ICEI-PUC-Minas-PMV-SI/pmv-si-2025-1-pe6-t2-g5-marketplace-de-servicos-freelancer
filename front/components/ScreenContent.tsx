import { Text, View } from 'react-native';

import { LoginRegister } from './LoginRegister';
import { SolicitarNovoServico } from './SolicitarNovoServico';
import ListagemServicos from './ListagemServicos';


type ScreenContentProps = {
  title: string;
  path: string;
  children?: React.ReactNode;
};

export const ScreenContent = ({ title, path, children }: ScreenContentProps) => {
  return (
    <View>
      <LoginRegister path={path} />
      {/* <SolicitarNovoServico path={path} /> */}
      {/* <ListagemServicos /> */}
      {children}
    </View>
  );
};