import { ProcessActions } from '@linn-it/linn-form-components-library';
import { printAllLabelsForProductActionTypes as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.printAllLabelsForProduct.item,
    processTypes.printAllLabelsForProduct.actionType,
    processTypes.printAllLabelsForProduct.uri,
    actionTypes,
    config.appRoot
);
