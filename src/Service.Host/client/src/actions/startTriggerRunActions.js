import { ProcessActions } from '@linn-it/linn-form-components-library';
import { startTriggerRunActionTypes as actionTypes } from './index';
import * as processTypes from '../processTypes';
import config from '../config';

export default new ProcessActions(
    processTypes.startTriggerRun.item,
    processTypes.startTriggerRun.actionType,
    processTypes.startTriggerRun.uri,
    actionTypes,
    config.appRoot
);
