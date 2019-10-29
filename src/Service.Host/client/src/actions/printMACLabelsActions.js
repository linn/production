import { ProcessActions } from '@linn-it/linn-form-components-library';
import { printMACLabelsActionTypes as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.printMACLabels.item,
    processTypes.printMACLabels.actionType,
    processTypes.printMACLabels.uri,
    actionTypes,
    config.appRoot
);
