import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { assemblyFailFaultCodeActionTypes as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.assemblyFailFaultCode.item,
    itemTypes.assemblyFailFaultCode.actionType,
    itemTypes.assemblyFailFaultCode.uri,
    actionTypes,
    config.appRoot
);
