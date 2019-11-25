import { ProcessActions } from '@linn-it/linn-form-components-library';
import { printWorksOrderAioLabelsActionTypes as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.printWorksOrderAioLabels.item,
    processTypes.printWorksOrderAioLabels.actionType,
    processTypes.printWorksOrderAioLabels.uri,
    actionTypes,
    config.appRoot
);
