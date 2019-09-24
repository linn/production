import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { ateFaultCodeActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.ateFaultCode.item,
    itemTypes.ateFaultCode.actionType,
    itemTypes.ateFaultCode.uri,
    actionTypes,
    config.appRoot
);
