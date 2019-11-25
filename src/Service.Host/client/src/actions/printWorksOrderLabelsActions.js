import { ProcessActions } from '@linn-it/linn-form-components-library';
import { printWorksOrderLabelsActionTypes as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.printWorksOrderLabels.item,
    processTypes.printWorksOrderLabels.actionType,
    processTypes.printWorksOrderLabels.uri,
    actionTypes,
    config.appRoot
);
