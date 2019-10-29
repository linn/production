import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { partFailFaultCodeActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.partFailFaultCode.item,
    itemTypes.partFailFaultCode.actionType,
    itemTypes.partFailFaultCode.uri,
    actionTypes,
    config.appRoot
);
